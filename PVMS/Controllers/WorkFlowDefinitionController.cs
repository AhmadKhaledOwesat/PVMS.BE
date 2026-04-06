using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using PVMS.Application.Dto;
using PVMS.Application.Interfaces;
using PVMS.Domain.Entities;
using PVMS.Domain.Entities.Filters;
using PVMS.Domain.Interfaces;

namespace PVMS.Controllers
{
    /// <summary>
    /// Dynamic workflow designer API. Frontend module key: <c>WorkFlowDefinition</c>.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [EnableCors("AllowAllOrigins")]
    public class WorkFlowDefinitionController(IWorkFlowDefinitionBll bll, IInnovaMapper mapper)
        : BaseController<WorkFlowDefinition, WorkFlowDefinitionDto, Guid, WorkFlowDefinitionFilter>(bll, mapper)
    {
        [HttpPost]
        [Route("")]
        [DisableRequestSizeLimit]
        public override async Task<InnovaResponse<Guid>> AddAsync([FromBody] WorkFlowDefinitionDto dto)
        {
            var (ok, msg, id) = await bll.SaveNewAsync(dto);
            if (!ok) return new InnovaResponse<Guid>(default, msg, false);
            return new InnovaResponse<Guid>(id);
        }

        [HttpPost]
        [Route("update")]
        [DisableRequestSizeLimit]
        public override async Task<InnovaResponse<Guid>> UpdateAsync([FromBody] WorkFlowDefinitionDto dto)
        {
            var (ok, msg, id) = await bll.SaveExistingAsync(dto);
            if (!ok) return new InnovaResponse<Guid>(default, msg, false);
            return new InnovaResponse<Guid>(id);
        }

        [HttpGet]
        [Route("{id}")]
        public override async Task<InnovaResponse<WorkFlowDefinitionDto>> GetByIdAsync([FromRoute] Guid id)
        {
            var dto = await bll.GetDetailDtoByIdAsync(id);
            if (dto == null) return new InnovaResponse<WorkFlowDefinitionDto>(default!, "Not found", false);
            return new InnovaResponse<WorkFlowDefinitionDto>(dto);
        }

        [HttpPost]
        [Route("search")]
        public override async Task<InnovaResponse<PageResult<WorkFlowDefinitionDto>>> GetAllAsync([FromBody] WorkFlowDefinitionFilter filter)
        {
            var page = await bll.GetPagedDtosAsync(filter);
            return new InnovaResponse<PageResult<WorkFlowDefinitionDto>>(page);
        }

        [HttpGet]
        [Route("mobile")]
        public async Task<InnovaResponse<List<WorkFlowDefinitionMobileDto>>> GetMobileAsync()
        {
            var data = await bll.GetMobileAsync();
            return new InnovaResponse<List<WorkFlowDefinitionMobileDto>>(data);
        }
    }
}
