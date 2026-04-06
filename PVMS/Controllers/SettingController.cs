using PVMS.Application.Dto;
using PVMS.Domain.Entities;
using PVMS.Domain.Entities.Filters;
using PVMS.Domain.Interfaces;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace PVMS.Controllers
{
    
    [ApiController]
    [Route("api/[controller]")]
    [EnableCors("AllowAllOrigins")]
    public class SettingController(ISettingBll settingBll, IInnovaMapper mapper) : BaseController<Setting, SettingDto, Guid, SettingFilter>(settingBll, mapper)
    {
        public override async Task<InnovaResponse<PageResult<SettingDto>>> GetAllAsync([FromBody] SettingFilter searchParameters)=> new InnovaResponse<PageResult<SettingDto>>(mapper.Map<PageResult<SettingDto>>(await settingBll.GetAllAsync(searchParameters)));

        [HttpGet]
        [Route("setting/{settingName}")]
        public async Task<InnovaResponse<PageResult<SettingDto>>> GetSettingsAsync([FromRoute] string settingName)
        {
            return new InnovaResponse<PageResult<SettingDto>>(mapper.Map<PageResult<SettingDto>>( await settingBll.GetSettingsAsync(settingName)));
        }

    
    }
}
