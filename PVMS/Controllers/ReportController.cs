using PVMS.Application.Dto;
using PVMS.Application.Interfaces;
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
    public class ReportController(IReportBll ReportBll, IInnovaMapper mapper) : BaseController<Report, ReportDto, Guid, ReportFilter>(ReportBll, mapper)
    {
        public override async Task<InnovaResponse<PageResult<ReportDto>>> GetAllAsync([FromBody] ReportFilter searchParameters)=> new InnovaResponse<PageResult<ReportDto>>(mapper.Map<PageResult<ReportDto>>(await ReportBll.GetAllAsync(searchParameters)));
       
        [HttpGet]
        [Route("excute/{query}")]
        public  async Task<InnovaResponse<dynamic>> ExecuteReportAsync([FromRoute] string query) => await ReportBll.ExecuteReport(query);

    }
}
