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
    public class TicketTypeController(ITicketTypeBll TaskStatusBll,IInnovaMapper mapper) : BaseController<TicketType,TicketTypeDto,Guid,TicketTypeFilter>(TaskStatusBll, mapper)
    {
        public override async Task<InnovaResponse<PageResult<TicketTypeDto>>> GetAllAsync([FromBody] TicketTypeFilter searchParameters)
        {
            return  new InnovaResponse<PageResult<TicketTypeDto>>(mapper.Map<PageResult<TicketTypeDto>>(await TaskStatusBll.GetAllAsync(searchParameters)));      
        }
    }
}
