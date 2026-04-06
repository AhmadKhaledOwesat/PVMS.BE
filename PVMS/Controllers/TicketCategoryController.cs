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
    public class TicketCategoryController(ITicketCategoryBll TaskStatusBll,IInnovaMapper mapper) : BaseController<TicketCategory,TicketCategoryDto,Guid,TicketCategoryFilter>(TaskStatusBll, mapper)
    {
        public override async Task<InnovaResponse<PageResult<TicketCategoryDto>>> GetAllAsync([FromBody] TicketCategoryFilter searchParameters)
        {
            return  new InnovaResponse<PageResult<TicketCategoryDto>>(mapper.Map<PageResult<TicketCategoryDto>>(await TaskStatusBll.GetAllAsync(searchParameters)));      
        }
    }
}
