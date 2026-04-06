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
    public class AppUserStatusController(IAppUserStatusBll appUserStatusBll, IDcpMapper mapper) : BaseController<AppUserStatus, AppUserStatusDto, Guid, AppUserStatusFilter>(appUserStatusBll, mapper)
    {
        public override async Task<DcpResponse<PageResult<AppUserStatusDto>>> GetAllAsync([FromBody] AppUserStatusFilter searchParameters)=> new DcpResponse<PageResult<AppUserStatusDto>>(mapper.Map<PageResult<AppUserStatusDto>>(await appUserStatusBll.GetAllAsync(searchParameters)));
    }
}
