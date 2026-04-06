using PVMS.Domain.Entities;
using PVMS.Domain.Entities.Filters;
using PVMS.Domain.Interfaces;
using PVMS.Infrastructure.Extensions;

namespace PVMS.Application.Bll
{
    public class SettingBll(IBaseDal<Setting, Guid, SettingFilter> baseDal) : BaseBll<Setting, Guid, SettingFilter>(baseDal), ISettingBll
    {
        public override async Task<PageResult<Setting>> GetAllAsync(SettingFilter searchParameters)
        {

            return await base.GetAllAsync(searchParameters);
        }

        public async Task<PageResult<Setting>> GetSettingsAsync(string settingName)
        {
            return await GetAllAsync(new SettingFilter {Term = settingName });
        }
    }
}
