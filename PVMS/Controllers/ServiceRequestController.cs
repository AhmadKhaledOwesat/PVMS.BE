using DcpTracker.Application.Dto;
using DcpTracker.Application.Interfaces;
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
    public class ServiceRequestController(IServiceRequestBll ServiceRequestBll,IDcpMapper mapper) : BaseController<ServiceRequest,ServiceRequestDto,Guid,ServiceRequestFilter>(ServiceRequestBll, mapper)
    {
        public override async Task<DcpResponse<PageResult<ServiceRequestDto>>> GetAllAsync([FromBody] ServiceRequestFilter searchParameters)
        {
            return  new DcpResponse<PageResult<ServiceRequestDto>>(mapper.Map<PageResult<ServiceRequestDto>>(await ServiceRequestBll.GetAllAsync(searchParameters)));      
        }
    }
}
