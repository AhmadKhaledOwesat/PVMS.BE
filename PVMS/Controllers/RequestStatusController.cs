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
    public class RequestStatusController(IRequestStatusBll RequestStatusBll, IDcpMapper mapper) : BaseController<RequestStatus, RequestStatusDto, int, RequestStatusFilter>(RequestStatusBll, mapper)
    {
        public override async Task<DcpResponse<PageResult<RequestStatusDto>>> GetAllAsync([FromBody] RequestStatusFilter searchParameters)=> new DcpResponse<PageResult<RequestStatusDto>>(mapper.Map<PageResult<RequestStatusDto>>(await RequestStatusBll.GetAllAsync(searchParameters)));
    }
}
