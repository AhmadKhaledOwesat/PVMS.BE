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
    public class GovernorateController(IGovernorateBll countryGovernorateBll, IDcpMapper mapper) : BaseController<Governorate, GovernorateDto, Guid, GovernorateFilter>(countryGovernorateBll, mapper)
    {
        public override async Task<DcpResponse<PageResult<GovernorateDto>>> GetAllAsync([FromBody] GovernorateFilter searchParameters)=> new DcpResponse<PageResult<GovernorateDto>>(mapper.Map<PageResult<GovernorateDto>>(await countryGovernorateBll.GetAllAsync(searchParameters)));
    }
}
