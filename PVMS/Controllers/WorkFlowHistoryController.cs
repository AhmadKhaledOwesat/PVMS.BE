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
    public class WorkFlowHistoryController(IWorkFlowHistoryBll TaskStatusBll,IInnovaMapper mapper) : BaseController<WorkFlowHistory,WorkFlowHistoryDto,Guid,WorkFlowHistoryFilter>(TaskStatusBll, mapper)
    {
        public override async Task<InnovaResponse<PageResult<WorkFlowHistoryDto>>> GetAllAsync([FromBody] WorkFlowHistoryFilter searchParameters)
        {
            return  new InnovaResponse<PageResult<WorkFlowHistoryDto>>(mapper.Map<PageResult<WorkFlowHistoryDto>>(await TaskStatusBll.GetAllAsync(searchParameters)));      
        }
    }
}
