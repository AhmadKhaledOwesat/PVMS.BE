using PVMS.Application.Dto;

namespace PVMS.Application.Interfaces
{
   public interface IAuthenticationManager
    {
        Tokens GenerateToken(string userName, object id);
    }
}
