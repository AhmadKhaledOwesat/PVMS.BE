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
    public class SubServiceController(ISubServiceBll SubServiceBll, IDcpMapper mapper) : BaseController<SubService, SubServiceDto, Guid, SubServiceFilter>(SubServiceBll, mapper)
    {
        public override async Task<DcpResponse<PageResult<SubServiceDto>>> GetAllAsync([FromBody] SubServiceFilter searchParameters) => new DcpResponse<PageResult<SubServiceDto>>(mapper.Map<PageResult<SubServiceDto>>(await SubServiceBll.GetAllAsync(searchParameters)));
    }
}
