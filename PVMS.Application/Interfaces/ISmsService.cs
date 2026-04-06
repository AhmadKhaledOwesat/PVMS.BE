using PVMS.Domain.Enum;

namespace PVMS.Application.Interfaces
{
    public interface ISmsService
    {
        Task<string> SendSmsAsync(string phoneNumber, string message,SmsType smsType);
    }
}
