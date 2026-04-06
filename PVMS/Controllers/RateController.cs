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
    public class RateController(IRateBll RateBll,IDcpMapper mapper) : BaseController<Rate,RateDto,Guid,RateFilter>(RateBll, mapper)
    {
        public override async Task<DcpResponse<PageResult<RateDto>>> GetAllAsync([FromBody] RateFilter searchParameters)
        {
            return  new DcpResponse<PageResult<RateDto>>(mapper.Map<PageResult<RateDto>>(await RateBll.GetAllAsync(searchParameters)));      
        }
    }
}
