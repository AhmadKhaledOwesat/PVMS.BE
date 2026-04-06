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
    public class RolePrivilegeController(IRolePrivilegeBll RolePrivilegeBll,IInnovaMapper mapper) : BaseController<RolePrivilege,RolePrivilegeDto,Guid,RolePrivilegeFilter>(RolePrivilegeBll, mapper)
    {
        public override async Task<InnovaResponse<PageResult<RolePrivilegeDto>>> GetAllAsync([FromBody] RolePrivilegeFilter searchParameters)
        {
            return  new InnovaResponse<PageResult<RolePrivilegeDto>>(mapper.Map<PageResult<RolePrivilegeDto>>(await RolePrivilegeBll.GetAllAsync(searchParameters)));      
        }
    }
}
