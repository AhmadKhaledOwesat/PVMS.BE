using DcpTracker.Domain.Entities;
using DcpTracker.Domain.Entities.Filters;
using DcpTracker.Domain.Interfaces;
using DcpTracker.Infrastructure.Extensions;

namespace DcpTracker.Application.Bll
{
    public class InquiryBll(IBaseDal<Inquiry,Guid, InquiryFilter> baseDal,IDcpMapper dcpMapper,IAppUserBll appUserBll) : BaseBll<Inquiry, Guid, InquiryFilter>(baseDal) , IInquiryBll
    {
        public override async Task AddAsync(Inquiry entity)
        {
            entity.Password = entity.Password.HashedPassword();
             await base.AddAsync(entity);   
        }
        public override Task<PageResult<Inquiry>> GetAllAsync(InquiryFilter searchParameters)
        {
            if(searchParameters is not null)
            {
                if(!searchParameters.FullName.IsNullOrEmpty())
                    searchParameters.Expression = new Func<Inquiry, bool>(a => a.FullName == searchParameters?.FullName);
            }

            return base.GetAllAsync(searchParameters);
        }

        public async Task<Guid> MoveToAppUser(Guid id)
        {
            var inquiry = await GetByIdAsync(id);   
            AppUsers appUsers  = dcpMapper.Map<AppUsers>(inquiry);
            appUsers.Id = default;
            await appUserBll.AddAsync(appUsers);    
            return appUsers.Id; 
        }
    }
}
