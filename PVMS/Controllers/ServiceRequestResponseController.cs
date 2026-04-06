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
    public class ServiceRequestResponseController(IServiceRequestResponseBll ServiceRequestResponseBll,IDcpMapper mapper) : BaseController<ServiceRequestResponse,ServiceRequestResponseDto,Guid,ServiceRequestResponseFilter>(ServiceRequestResponseBll, mapper)
    {
        public override async Task<DcpResponse<PageResult<ServiceRequestResponseDto>>> GetAllAsync([FromBody] ServiceRequestResponseFilter searchParameters)
        {
            return  new DcpResponse<PageResult<ServiceRequestResponseDto>>(mapper.Map<PageResult<ServiceRequestResponseDto>>(await ServiceRequestResponseBll.GetAllAsync(searchParameters)));      
        }
    }
}
