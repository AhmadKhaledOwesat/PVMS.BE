using PVMS.Application.Dto;
using PVMS.Application.Interfaces;
using PVMS.Domain.Entities;
using PVMS.Domain.Entities.Filters;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using PVMS.Domain.Interfaces;

namespace PVMS.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [EnableCors("AllowAllOrigins")]
    public class AditLogController(IAditLogBll aditLogBll, IInnovaMapper mapper) : ControllerBase
    {
        [HttpGet("{id}")]
        public async Task<InnovaResponse<AditLogDto>> GetByIdAsync([FromRoute] Guid id)
        {
            var entity = await aditLogBll.GetByIdAsync(id);
            if (entity == null)
                return new InnovaResponse<AditLogDto>(null!, "Not found", false);
            return new InnovaResponse<AditLogDto>(mapper.Map<AditLogDto>(entity));
        }

        [HttpPost("search")]
        public async Task<InnovaResponse<PageResult<AditLogDto>>> GetAllAsync([FromBody] AditLogFilter filter)
        {
            var result = await aditLogBll.GetAllAsync(filter ?? new AditLogFilter());
            return new InnovaResponse<PageResult<AditLogDto>>(mapper.Map<PageResult<AditLogDto>>(result));
        }
    }
}
