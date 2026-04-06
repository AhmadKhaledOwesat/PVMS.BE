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
    public class ProcedureTypeController(IProcedureTypeBll TaskStatusBll,IInnovaMapper mapper) : BaseController<ProcedureType,ProcedureTypeDto,Guid,ProcedureTypeFilter>(TaskStatusBll, mapper)
    {
        public override async Task<InnovaResponse<PageResult<ProcedureTypeDto>>> GetAllAsync([FromBody] ProcedureTypeFilter searchParameters)
        {
            return  new InnovaResponse<PageResult<ProcedureTypeDto>>(mapper.Map<PageResult<ProcedureTypeDto>>(await TaskStatusBll.GetAllAsync(searchParameters)));      
        }
    }
}
