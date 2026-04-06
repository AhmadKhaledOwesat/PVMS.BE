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
    public class MainSliderController(IMainSliderBll mainSliderBll, IDcpMapper mapper) : BaseController<MainSlider, MainSliderDto, Guid, MainSliderFilter>(mainSliderBll, mapper)
    {
        public override async Task<DcpResponse<PageResult<MainSliderDto>>> GetAllAsync([FromBody] MainSliderFilter searchParameters) => new DcpResponse<PageResult<MainSliderDto>>(mapper.Map<PageResult<MainSliderDto>>(await mainSliderBll.GetAllAsync(searchParameters)));
    }
}
