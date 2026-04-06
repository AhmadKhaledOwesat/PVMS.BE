using DcpTracker.Application.Interfaces;
using DcpTracker.Domain.Entities;
using DcpTracker.Domain.Entities.Filters;
using DcpTracker.Domain.Interfaces;
using DcpTracker.Infrastructure.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Linq.Expressions;

namespace DcpTracker.Application.Bll
{
    public class ServiceRequestBll(IRequestStatusBll requestStatusBll, IAppUserBll appUserBll, IConfiguration configuration, IBaseDal<ServiceRequest, Guid, ServiceRequestFilter> baseDal, IDcpMapper dcpMapper, IServiceRequestResponseBll serviceRequestResponseBll) : BaseBll<ServiceRequest, Guid, ServiceRequestFilter>(baseDal), IServiceRequestBll
    {
        public override async Task<PageResult<ServiceRequest>> GetAllAsync(ServiceRequestFilter searchParameters)
        {
            if (searchParameters is not null)
            {
                if ((searchParameters.ClientId ?? Guid.Empty) != Guid.Empty)
                    searchParameters.Expression = new Func<ServiceRequest, bool>(a => a.ClientId == searchParameters.ClientId);
            }

            return await base.GetAllAsync(searchParameters);
        }
        public override async Task AddAsync(ServiceRequest entity)
        {
            if (!entity.AttachmentFile.IsNullOrEmpty())
                entity.AttachmentFile = await entity.AttachmentFile.SaveImage(entity.AttachmentFileType);
            Expression<Func<ServiceRequest, bool>> expression = x => x.ClientId != null;
            int maxRequestNumber = (await base.GetCountByExpressionAsync(expression)) + 1;
            entity.RequestCode = $"{DateTime.Now:yyyy}{maxRequestNumber.ToString().PadLeft(5, '0')}";
            entity.RequestStatus = null;
            entity.RequestStatusId = 1;
            await base.AddAsync(entity);
            ServiceRequestResponse serviceRequestResponse = dcpMapper.Map<ServiceRequestResponse>(entity);
            serviceRequestResponse.Id = default;
            serviceRequestResponse.RequestStatus = null;
            await serviceRequestResponseBll.AddAsync(serviceRequestResponse);
            string projectId = configuration.GetSection("fcmProjectId").Value;
            if (!string.IsNullOrEmpty(projectId))
            {
                string path = configuration.GetSection("adminSdkPath").Value;
                var sender = new FcmSender(path, projectId);
               /// var allProviders = await appUserBll.GetAllAsync(new(){ Expression = new Func<AppUsers, bool>(a => a.PushNotificationToken != null && a.AppUserTypeId == Domain.Enum.AppUserType.Provider) });
               // foreach (var provider in allProviders.Collections)
                   // await sender.SendNotificationAsync(provider.PushNotificationToken, "Total Care", "يوجد طلب جديد الرجاء الذهاب الى قائمة الطلبات");
            }
        }
        public override async Task UpdateAsync(ServiceRequest entity)
        {
            ServiceRequest serviceRequest = await GetByIdAsync(entity.Id);
            serviceRequest.RequestStatus = null;
            if (!entity.AttachmentFile.IsNullOrEmpty() && serviceRequest.AttachmentFile != entity.AttachmentFile)
                entity.AttachmentFile = await entity.AttachmentFile.SaveImage(entity.AttachmentFileType);
            else
                entity.AttachmentFile = serviceRequest.AttachmentFile;

            if (entity.RequestStatusId != serviceRequest.RequestStatusId)
            {
                ServiceRequestResponse serviceRequestResponse = dcpMapper.Map<ServiceRequestResponse>(entity);
                serviceRequestResponse.Id = default;
                serviceRequestResponse.RequestStatus = null;
                serviceRequestResponse.ResponseTime = TimeOnly.FromDateTime(DateTime.Now);
                serviceRequestResponse.ResponseDate =DateOnly.FromDateTime(DateTime.Now);
                await serviceRequestResponseBll.AddAsync(serviceRequestResponse);
                string projectId = configuration.GetSection("fcmProjectId").Value;
                RequestStatus requestStatus = await requestStatusBll.GetByIdAsync(entity.RequestStatusId.Value);
                if (!string.IsNullOrEmpty(projectId))
                {
                    string path = configuration.GetSection("adminSdkPath").Value;
                    var sender = new FcmSender(path, projectId);
                    AppUsers appUser=null;
                    if (requestStatus.NotifyClient == 1)
                        appUser = await appUserBll.GetByIdAsync(entity.ClientId.Value);

                    if(requestStatus.NotifyProvider == 1 && entity.ProviderId.HasValue && entity.ProviderId.Value != Guid.Empty)
                        appUser = await appUserBll.GetByIdAsync(entity.ProviderId.Value);

                    if (appUser != null && appUser.PushNotificationToken != null)
                        await sender.SendNotificationAsync(appUser.PushNotificationToken, "Total Care", $" تم تحديث حالة طلبك الى {requestStatus.NameAr}");
                }
                entity.RequestStatus = null;
            }
            entity.RequestCode = serviceRequest.RequestCode;
            await base.UpdateAsync(entity);
        }

        public async Task<DcpResponse<bool>> UpdateLocation(Guid requestId , string fieldName , string gps)
        {

            ServiceRequest serviceRequest = await GetByIdAsync(requestId);  
            serviceRequest.GetType().GetProperty(fieldName).SetValue(serviceRequest, gps);
            await base.UpdateAsync(serviceRequest);
            return new DcpResponse<bool>(true, IsSuccess: true);
        }
    }
}
