using DcpTracker.Application.Dto;
using DcpTracker.Domain.Entities;
using DcpTracker.Domain.Entities.Filters;
using DcpTracker.Domain.Interfaces;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace DcpTracker.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("api/[controller]")]
    [EnableCors("AllowAllOrigins")]
    public class MainSubServiceMappingController(IMainSubServiceMappingBll MainSubServiceMappingBll,IDcpMapper mapper) : BaseController<MainSubServiceMapping,MainSubServiceMappingDto,Guid,MainSubServiceMappingFilter>(MainSubServiceMappingBll, mapper)
    {
        public override async Task<DcpResponse<PageResult<MainSubServiceMappingDto>>> GetAllAsync([FromBody] MainSubServiceMappingFilter searchParameters)
        {
            return  new DcpResponse<PageResult<MainSubServiceMappingDto>>(mapper.Map<PageResult<MainSubServiceMappingDto>>(await MainSubServiceMappingBll.GetAllAsync(searchParameters)));      
        }
    }
}
