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
    public class PrivilegeController(IPrivilegeBll PrivilegeBll,IInnovaMapper mapper) : BaseController<Privilege,PrivilegeDto,Guid,PrivilegeFilter>(PrivilegeBll, mapper)
    {
        public override async Task<InnovaResponse<PageResult<PrivilegeDto>>> GetAllAsync([FromBody] PrivilegeFilter searchParameters)
        {
            return  new InnovaResponse<PageResult<PrivilegeDto>>(mapper.Map<PageResult<PrivilegeDto>>(await PrivilegeBll.GetAllAsync(searchParameters)));      
        }
    }
}
