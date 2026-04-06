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
    public class TicketAttachmentController(ITicketAttachmentBll TicketAttachmentBll,IInnovaMapper mapper) : BaseController<TicketAttachment,TicketAttachmentDto,Guid,TicketAttachmentFilter>(TicketAttachmentBll, mapper)
    {
        public override async Task<InnovaResponse<PageResult<TicketAttachmentDto>>> GetAllAsync([FromBody] TicketAttachmentFilter searchParameters)
        {
            return  new InnovaResponse<PageResult<TicketAttachmentDto>>(mapper.Map<PageResult<TicketAttachmentDto>>(await TicketAttachmentBll.GetAllAsync(searchParameters)));      
        }
    }
}
