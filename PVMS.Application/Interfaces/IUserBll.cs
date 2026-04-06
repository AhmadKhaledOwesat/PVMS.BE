using PVMS.Application.Dto;
using PVMS.Domain.Entities;
using PVMS.Domain.Entities.Filters;
using PVMS.Domain.Interfaces;

namespace PVMS.Application.Interfaces
{
    public interface IUserBll : IBaseBll<Users, Guid, UserFilter>
    {
        Task<InnovaResponse<UsersDto>> LoginAsync(string userName, string password);
        Task<InnovaResponse<string>> ResetPasswordAsync(string userName);
        Task<InnovaResponse<string>> UpdatePasswordAsync(string userName, string newPassword);
        Task<InnovaResponse<bool>> UpdateLocationAsync(Guid id, string currentLocation);
    }
}
