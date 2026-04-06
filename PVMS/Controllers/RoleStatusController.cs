using PVMS.Application.Dto;
using PVMS.Domain.Entities;
using PVMS.Domain.Entities.Filters;
using PVMS.Domain.Interfaces;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using PVMS.Application.Interfaces;

namespace PVMS.Controllers
{
    
    [ApiController]
    [Route("api/[controller]")]
    [EnableCors("AllowAllOrigins")]
    public class RoleStatusController(IRoleStatusBll RoleStatusBll,IInnovaMapper mapper) : BaseController<RoleStatus,RoleStatusDto,Guid,RoleStatusFilter>(RoleStatusBll, mapper)
    {
        public override async Task<InnovaResponse<PageResult<RoleStatusDto>>> GetAllAsync([FromBody] RoleStatusFilter searchParameters)
        {
            return  new InnovaResponse<PageResult<RoleStatusDto>>(mapper.Map<PageResult<RoleStatusDto>>(await RoleStatusBll.GetAllAsync(searchParameters)));      
        }
    }
}
