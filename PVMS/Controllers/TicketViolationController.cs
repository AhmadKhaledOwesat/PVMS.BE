using PVMS.Application.Dto;
using PVMS.Domain.Entities;
using PVMS.Domain.Entities.Filters;
using PVMS.Domain.Interfaces;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using PVMS.Application.Interfaces;

namespace PVMS.Controllers
{
    
    [ApiController]
    [Route("api/[controller]")]
    [EnableCors("AllowAllOrigins")]
    public class TicketViolationController(ITicketViolationBll TicketViolationBll,IInnovaMapper mapper) : BaseController<TicketViolation,TicketViolationDto,Guid,TicketViolationFilter>(TicketViolationBll, mapper)
    {
        public override async Task<InnovaResponse<PageResult<TicketViolationDto>>> GetAllAsync([FromBody] TicketViolationFilter searchParameters)
        {
            return  new InnovaResponse<PageResult<TicketViolationDto>>(mapper.Map<PageResult<TicketViolationDto>>(await TicketViolationBll.GetAllAsync(searchParameters)));      
        }
    }
}
