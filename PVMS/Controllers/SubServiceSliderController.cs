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
    public class SubServiceSliderController(ISubServiceSliderBll SubServiceSliderBll, IDcpMapper mapper) : BaseController<SubServiceSlider, SubServiceSliderDto, Guid, SubServiceSliderFilter>(SubServiceSliderBll, mapper)
    {
        public override async Task<DcpResponse<PageResult<SubServiceSliderDto>>> GetAllAsync([FromBody] SubServiceSliderFilter searchParameters)=> new DcpResponse<PageResult<SubServiceSliderDto>>(mapper.Map<PageResult<SubServiceSliderDto>>(await SubServiceSliderBll.GetAllAsync(searchParameters)));
    }
}
