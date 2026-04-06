using Azure.Messaging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;
using PVMS.Application.Dto;
using PVMS.Application.Interfaces;
using PVMS.Domain.Enum;
using System.Net.Http.Headers;
using System.Text;

namespace PVMS.Application.Services
{
    public class SmsService(IOptions<SmsOptions> options, ISmsHistoryBll smsHistoryBll, IIdentityManager<Guid> identityManager) : ISmsService
    {
        private static string BuildMessage(SmsType smsType, string content)
        {
            switch (smsType)
            {
                case SmsType.Otp:
                    return $"Your Verification Code is : {content} , do not share it with anyone"; ;
                case SmsType.Message:
                    break;
                default:
                    throw new ArgumentException("Type not supported");
            }
            return content;
        }
        public async Task<string> SendSmsAsync(string phoneNumber, string message, SmsType smsType)
        {
            var status = "";
            var messageContent = BuildMessage(smsType, message);

            try
            {
                var credentials = Convert.ToBase64String(
                    Encoding.ASCII.GetBytes($"{options.Value.UserName}:{options.Value.Password}")
                );

                using var client = new HttpClient();
                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Basic", credentials);

                var content = new FormUrlEncodedContent(new Dictionary<string, string>
                {
                    ["mobile_number"] = phoneNumber,
                    ["msg"] = messageContent,
                    ["from"] = "Petra-pra",
                    ["tag"] = "1"
                });

                var response = await client.PostAsync(options.Value.BaseUrl, content);
                var responseString = await response.Content.ReadAsStringAsync();

                 status = JObject.Parse(responseString)["status"]?.ToString();

                return status;
            }
            catch (Exception ex)
            {
                status =  ex.InnerException?.Message ?? ex.Message;
            }
            finally
            {
                await smsHistoryBll.AddAsync(new Domain.Entities.SmsHistory
                {
                    Message = smsType == SmsType.Otp ? message : messageContent,
                    IsVerified = 0,
                    Note = status,
                    TypeId = smsType,
                    UserId = identityManager.CurrentUserId
                });
            }
            return status;
        }

    }
}
