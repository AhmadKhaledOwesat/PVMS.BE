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
    public class UserRoleController(IUserRoleBll UserRoleBll,IInnovaMapper mapper) : BaseController<UserRole,UserRoleDto,Guid,UserRoleFilter>(UserRoleBll, mapper)
    {
        public override async Task<InnovaResponse<PageResult<UserRoleDto>>> GetAllAsync([FromBody] UserRoleFilter searchParameters)
        {
            return  new InnovaResponse<PageResult<UserRoleDto>>(mapper.Map<PageResult<UserRoleDto>>(await UserRoleBll.GetAllAsync(searchParameters)));      
        }
    }
}
