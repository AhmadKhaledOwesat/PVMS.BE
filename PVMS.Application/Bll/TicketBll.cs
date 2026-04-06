using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using PVMS.Application.Dto;
using PVMS.Application.Helpers;
using PVMS.Application.Interfaces;
using PVMS.Domain.Entities;
using PVMS.Domain.Entities.Filters;
using PVMS.Domain.Enum;
using PVMS.Domain.Interfaces;
using PVMS.Infrastructure.Extensions;
using QRCoder;
using RestSharp;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.Net;
using System.Threading.Tasks;
namespace PVMS.Application.Bll
{
    public class TicketBll(IBaseDal<Ticket, Guid, TicketFilter> baseDal,ISmsHistoryBll smsHistoryBll, IUserBll userBll, ITicketViolationBll ticketViolationBll, IOptions<QrCodeOptions> qrCodeOptions, IHostEnvironment hostEnvironment, IReportService reportService, IWorkFlowHistoryBll workFlowHistoryBll, INotificationBll notificationBll) : BaseBll<Ticket, Guid, TicketFilter>(baseDal), ITicketBll
    {
        public async Task<QrCodeDto> GetDetailsAsync(QrCodeDto entity)
        {
            Ticket ticket = await GetByIdAsync(Guid.Parse(entity.TicketId));
            entity.TicketDate = ticket.TicketDate.ToString();
            entity.TicketNo = ticket.TicketNo.ToString();
            entity.FullName = ticket.Citizen?.FullName.ToString();
            entity.TicketId = ticket.Id.ToString();
            return entity;
        }
        public async Task<LoginResultDto> LoginAsync()
        {
            RestClient client = new(qrCodeOptions.Value.LoginUrl);
            var request = new RestRequest
            {
                Method = Method.Post
            };
            request.AddHeader("Content-Type", "application/json");
            var body = @"{" + "\n" + @"  ""email"":" + "\"" + qrCodeOptions.Value.Email + "\"" + ",\n" + @"  ""password"": " + "\"" + qrCodeOptions.Value.Password + "\"" + "\n" + @"}";
            request.AddParameter("application/json", body, ParameterType.RequestBody);
            RestResponse response = await client.ExecuteAsync(request);
            return JsonConvert.DeserializeObject<LoginResultDto>(response.Content.ToString());
        }
        private static string GetDarftText(TicketStatus ticketStatus)
        {
            return ticketStatus switch
            {
                TicketStatus.New or TicketStatus.UnderReview or TicketStatus.BactToSite => "مسودة المشرف",
                TicketStatus.UnderVerification => "مسودة العمليات والسيطرة",
                TicketStatus.LegalApproval => "مسودة الجهة القانونية",
                _ => string.Empty,
            };
        }
        public async Task<byte[]> GenerateReportAsync(Guid id)
        {
            string wwwroot = Path.Combine(hostEnvironment.ContentRootPath, "wwwroot");

            string filePath = Path.Combine(wwwroot, "Reports", "Report.rdlc");

            var data = await GetByIdAsync(id);

            var culture = new CultureInfo("ar-JO");
            byte[] bytes = [];
            string qrCode = "";
            if (data.QrCode.IsNullOrEmpty())
            {
                (bytes, qrCode) = await QrCodeAsync(id, data);
            }
            else
            {
                bytes = GenerateQrCodeImage(data.QrCode);
            }

            string draft = GetDarftText((TicketStatus)data.StatusId);

            var parameters = new Dictionary<string, string>{
                 { "FullName", data.Citizen?.FullName },
                 { "NationalityName", data.Citizen?.Nationality?.NameAr ?? "غيـــر معرف" },
                 { "Gender", (data.Citizen?.GenderId == 1? "ذكر" : "انثى") },
                 { "NationalId", data.Citizen?.NationalId },
                 { "DayName", data.CreatedDate.ToString("dddd",culture) },
                 { "Date",  data.CreatedDate.ToString("dd-MM-yyyy") },
                 { "Time", data.CreatedDate.ToString("hh:mm tt",culture) },
                 { "Location", data.Location?.NameAr ?? "غيـــر معرف" },
                 { "UserName", data.Inspector?.FullName ?? "غيـــر معرف" },
                 { "MobileNo", data.Inspector?.MobileNo ?? "غير معرف" },
                 { "TotalAmount", data.TicketViolations.Max(a=>a.Amount).ToString() ?? "0.00"  },
                 { "ImageBase64", Convert.ToBase64String(bytes)  },
                 { "Draft", draft },
                 { "Number", data.TicketNo ?? "غير معرف" },
            };

            var violations = data.TicketViolations
                .Select((a, index) => new
                {
                    Subject = $"{a.TicketType?.NameAr} ",
                    Id = ArabicNumberHelper.ToArabicDigits(index + 1)
                })
                .ToList();
            byte[] pdf = reportService.GeneratePdfAsync(filePath, "ViolationDbSet", violations, parameters);

            return pdf;
        }
#pragma warning disable CA1416 // Validate platform compatibility
        private async Task<(byte[],string)> QrCodeAsync(Guid id,Ticket ticket)
        {
            LoginResultDto loginResult = await LoginAsync();
            var client = new RestClient(qrCodeOptions.Value.BuildUrl);
            var request = new RestRequest();
            request.AddHeader("Authorization", $"Bearer {loginResult.tooken}");
            request.AddHeader("qr_include_keys", $"true");
            request.Method = Method.Post;
            request.AddHeader("Content-Type", "application/json");
            QrCodeDto qrCodeDto = new()
            {
                TicketId = id.ToString()
            };
            var body = JsonConvert.SerializeObject(qrCodeDto);
            request.AddParameter("application/json", body, ParameterType.RequestBody);
#pragma warning disable SYSLIB0014 // Type or member is obsolete
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
#pragma warning restore SYSLIB0014 // Type or member is obsolete
            RestResponse response = await client.ExecuteAsync(request);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                return await HandleQrCodeData(response, ticket);
            }
            return ([],string.Empty);
#pragma warning restore CA1416 // Validate platform compatibility
        }
        private  async Task<(byte[], string)> HandleQrCodeData(RestResponse response,Ticket ticket)
        {
            var qrCode = JsonConvert.DeserializeObject<MOOQrCodeDto>(response.Content.ToString());
            byte[] byteImage =  GenerateQrCodeImage(qrCode.QrCode);
            ticket.QrCode = qrCode.QrCode;
            await base.UpdateAsync(ticket);
            return (byteImage, qrCode.QrCode);
        }

        private static byte[] GenerateQrCodeImage(string qrCode)
        {
            QRCodeGenerator qrGenerator = new();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(qrCode, QRCodeGenerator.ECCLevel.Q);
            QRCode qrCodeValue = new(qrCodeData);
            MemoryStream ms = new();
            Bitmap qrCodeImage = qrCodeValue.GetGraphic(2);
            qrCodeImage.Save(ms, ImageFormat.Png);
            byte[] byteImage = ms.ToArray();
            return byteImage;
        }

        public override async Task<PageResult<Ticket>> GetAllAsync(TicketFilter searchParameters)
        {
            searchParameters.Expression = new Func<Ticket, bool>(t =>
            (searchParameters.StatusId == null || t.StatusId == searchParameters.StatusId) &&
            (searchParameters.TicketNo == null || t.TicketNo.Contains(searchParameters.TicketNo)) &&
            (searchParameters.FromDate == null || t.CreatedDate >= searchParameters.FromDate) &&
            (searchParameters.ToDate == null || t.CreatedDate <= searchParameters.ToDate) &&
            (searchParameters.InspectorId == null || t.CreatedBy == searchParameters.InspectorId));

            var data = await base.GetAllAsync(searchParameters);
            data.Collections = [.. data.Collections.OrderByDescending(a => a.CreatedDate)];
            return data;
        }
        public override async Task UpdateAsync(Ticket entity)
        {
            var dbEntity = await GetByIdAsync(entity.Id);

            if (dbEntity.StatusId != entity.StatusId)
            {
                await ExecuteWorkFlowAsync(entity, dbEntity);
                if (dbEntity.StatusId == -1)
                {
                    dbEntity.Note = entity.Note;
                    dbEntity.LocationId = entity.LocationId;
                    dbEntity.CitizenId = entity.CitizenId;
                    dbEntity.Coordination = entity.Coordination;
                    dbEntity.TicketDate = entity.TicketDate;
                    dbEntity.QrCode = null;
                    await HandleTicketViolationAsync(entity);
                }
                dbEntity.StatusId = entity.StatusId;

                if ((TicketStatus)entity.StatusId != TicketStatus.BactToSite)
                    await AddNotificationsAsync([dbEntity], (TicketStatus)entity.StatusId);
            }
            await base.UpdateAsync(dbEntity);

        }

        private async Task HandleTicketViolationAsync(Ticket entity)
        {
            if (entity.TicketViolations.Count > 0)
            {
                var oldViolations = await ticketViolationBll.FindAllByExpressionAsync(a => a.TicketId == entity.Id);
                if (oldViolations.Count > 0)
                {
                    await ticketViolationBll.DeleteRangeAsync(oldViolations);
                }
                foreach (var item in entity.TicketViolations)
                {
                    item.TicketId = entity.Id;
                    item.TicketType = null;
                }

                await ticketViolationBll.AddRangeAsync(entity.TicketViolations);
            }
        }

        private async Task ExecuteWorkFlowAsync(Ticket entity, Ticket dbEntity)
        {
            await workFlowHistoryBll.AddAsync(new WorkFlowHistory
            {
                NewStatusId = entity.StatusId,
                OldStatusId = dbEntity.StatusId,
                TicketId = entity.Id,
                Note = entity.BackOfficeNote ?? entity.Note,
                ProcedureTypeId = entity.ProcedureTypeId,
                IsSkip = entity.IsSkip
            });
        }

        public override Task AddAsync(Ticket entity)
        {
            return AddRangeAsync([entity]);
        }
        public override async Task AddRangeAsync(List<Ticket> entities)
        {
            var maxCount = await GetCountByExpressionAsync(a => a.CreatedDate.Year == DateTime.Now.Year);
            foreach (var item in entities)
            {
                int finalValue = maxCount + 1;
                item.TicketNo = $"{DateTime.Now.Year}/{finalValue.ToString().PadLeft(6, '0')}";
                await StartWorkFlowAsync(item);
            }
            await base.AddRangeAsync(entities);
            await AddNotificationsAsync(entities, TicketStatus.UnderReview);
        }
        private async Task StartWorkFlowAsync(Ticket item)
        {
            await workFlowHistoryBll.AddAsync(new WorkFlowHistory
            {
                NewStatusId = 1,
                TicketId = item.Id,
                CreatedBy = item.CreatedBy,
            });
        }
        private async Task AddNotificationsAsync(List<Ticket> tickets, TicketStatus ticketStatus)
        {
            var users = await userBll.FindAllByExpressionAsync(u => u.UserRoles.Any(ur => ur.Role.RoleStatus.Any(rs => rs.StatusId == (int)ticketStatus)));

            List<Notifications> notifications = [.. tickets.SelectMany(ticket => users.Select(user => new Notifications
                {
                    Title = "ضبط جديد",
                    Body = ticket.TicketNo,
                    EntityId = ticket.Id,
                    IsRead = 0,
                    UserId = user.Id
                }))];

            if (notifications.Count > 0)
                await notificationBll.AddRangeAsync(notifications, ticketStatus);
        }
    }
}
