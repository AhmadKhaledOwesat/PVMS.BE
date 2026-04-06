using PVMS.Application.Dto;
using PVMS.Domain.Entities;
using PVMS.Domain.Entities.Filters;
using PVMS.Domain.Interfaces;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace PVMS.Controllers
{
    
    [ApiController]
    [Route("api/[controller]")]
    [EnableCors("AllowAllOrigins")]
    public class ReportParameterController(IReportParameterBll ReportParameterBll, IInnovaMapper mapper) : BaseController<ReportParameter, ReportParameterDto, Guid, ReportParameterFilter>(ReportParameterBll, mapper)
    {
        public override async Task<InnovaResponse<PageResult<ReportParameterDto>>> GetAllAsync([FromBody] ReportParameterFilter searchParameters) => new InnovaResponse<PageResult<ReportParameterDto>>(mapper.Map<PageResult<ReportParameterDto>>(await ReportParameterBll.GetAllAsync(searchParameters)));
    }
}
