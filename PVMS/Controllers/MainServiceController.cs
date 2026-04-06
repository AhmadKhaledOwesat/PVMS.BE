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
    //[EnableCors("AllowAllOrigins")]
    public class MainServiceController(IMainServiceBll MainServiceBll, IDcpMapper mapper) : BaseController<MainService, MainServiceDto, Guid, MainServiceFilter>(MainServiceBll, mapper)
    {
        public override async Task<DcpResponse<PageResult<MainServiceDto>>> GetAllAsync([FromBody] MainServiceFilter searchParameters) => new DcpResponse<PageResult<MainServiceDto>>(mapper.Map<PageResult<MainServiceDto>>(await MainServiceBll.GetAllAsync(searchParameters)));
    }
}
