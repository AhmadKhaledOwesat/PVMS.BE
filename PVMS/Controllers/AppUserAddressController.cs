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
    public class AppUserAddressController(IAppUserAddressBll AppUserAddressBll, IDcpMapper mapper) : BaseController<AppUserAddress, AppUserAddressDto, Guid, AppUserAddressFilter>(AppUserAddressBll, mapper)
    {
        public override async Task<DcpResponse<PageResult<AppUserAddressDto>>> GetAllAsync([FromBody] AppUserAddressFilter searchParameters)=> new DcpResponse<PageResult<AppUserAddressDto>>(mapper.Map<PageResult<AppUserAddressDto>>(await AppUserAddressBll.GetAllAsync(searchParameters)));
    }
}
