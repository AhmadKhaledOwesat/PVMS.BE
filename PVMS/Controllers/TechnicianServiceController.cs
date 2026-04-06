using DcpTracker.Application.Dto;
using DcpTracker.Domain.Entities;
using DcpTracker.Domain.Entities.Filters;
using DcpTracker.Domain.Interfaces;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using TechnicianService = DcpTracker.Domain.Entities.TechnicianService;

namespace DcpTracker.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("api/[controller]")]
    [EnableCors("AllowAllOrigins")]
    public class TechnicianServiceController(ITechnicianServiceBll TechnicianServiceBll,IDcpMapper mapper) : BaseController<TechnicianService,TechnicianServiceDto,Guid,TechnicianServiceFilter>(TechnicianServiceBll, mapper)
    {
        public override async Task<DcpResponse<PageResult<TechnicianServiceDto>>> GetAllAsync([FromBody] TechnicianServiceFilter searchParameters)
        {
            return  new DcpResponse<PageResult<TechnicianServiceDto>>(mapper.Map<PageResult<TechnicianServiceDto>>(await TechnicianServiceBll.GetAllAsync(searchParameters)));      
        }
    }
}
