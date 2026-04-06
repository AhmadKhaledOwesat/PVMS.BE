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
    public class TicketTypePriceController(ITicketTypePriceBll ticketTypePriceBll, IInnovaMapper mapper) : BaseController<TicketTypePrice,TicketTypePriceDto,Guid,TaskTypePriceFilter>(ticketTypePriceBll, mapper)
    {
        public override async Task<InnovaResponse<PageResult<TicketTypePriceDto>>> GetAllAsync([FromBody] TaskTypePriceFilter searchParameters)
        {
            return  new InnovaResponse<PageResult<TicketTypePriceDto>>(mapper.Map<PageResult<TicketTypePriceDto>>(await ticketTypePriceBll.GetAllAsync(searchParameters)));      
        }
    }
}
