using PVMS.Domain.Entities;
using PVMS.Domain.Entities.Filters;

namespace PVMS.Domain.Interfaces
{
    public interface ISettingBll : IBaseBll<Setting, Guid, SettingFilter>
    {
        Task<PageResult<Setting>> GetSettingsAsync(string settingName);
    }
}
