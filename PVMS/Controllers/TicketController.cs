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
    public class TicketController(ITicketBll ticketBll,IInnovaMapper mapper) : BaseController<Ticket, TicketDto,Guid,TicketFilter>(ticketBll, mapper)
    {
        public override async Task<InnovaResponse<PageResult<TicketDto>>> GetAllAsync([FromBody] TicketFilter searchParameters)
        {
            return  new InnovaResponse<PageResult<TicketDto>>(mapper.Map<PageResult<TicketDto>>(await ticketBll.GetAllAsync(searchParameters)));      
        }

        [HttpGet]
        [Route("report/{id}")]
        public async  Task<IActionResult> GenerateReportAsync(Guid id)
        {
            return File(await ticketBll.GenerateReportAsync(id), "application/pdf", $"Ticket_{id}.pdf");
        }

        [HttpPost]
        [Route("qrCode")]
        public async Task<QrCodeDto> GetDetailsAsync([FromBody] QrCodeDto qrCodeDto)
        {
            return await ticketBll.GetDetailsAsync(qrCodeDto);
        }

    }
}
