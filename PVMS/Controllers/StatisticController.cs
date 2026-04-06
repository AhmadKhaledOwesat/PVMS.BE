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
    public class StatisticController(IStatisticBll StatisticBll, IInnovaMapper mapper) : BaseController<Statistic, StatisticDto, Guid, StatisticFilter>(StatisticBll, mapper)
    {
        public override async Task<InnovaResponse<PageResult<StatisticDto>>> GetAllAsync([FromBody] StatisticFilter searchParameters) => new InnovaResponse<PageResult<StatisticDto>>(mapper.Map<PageResult<StatisticDto>>(await StatisticBll.GetAllAsync(searchParameters)));
    }
}
