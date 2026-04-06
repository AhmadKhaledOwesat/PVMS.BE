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
    public class RoleController(IRoleBll RoleBll,IInnovaMapper mapper) : BaseController<Role,RoleDto,Guid,RoleFilter>(RoleBll, mapper)
    {
        public override async Task<InnovaResponse<PageResult<RoleDto>>> GetAllAsync([FromBody] RoleFilter searchParameters)
        {
            return  new InnovaResponse<PageResult<RoleDto>>(mapper.Map<PageResult<RoleDto>>(await RoleBll.GetAllAsync(searchParameters)));      
        }
    }
}
