using DcpTracker.Application.Dto;
using DcpTracker.Application.Interfaces;
using DcpTracker.Domain.Entities;
using DcpTracker.Domain.Entities.Filters;
using DcpTracker.Domain.Interfaces;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace DcpTracker.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("api/[controller]")]
    [EnableCors("AllowAllOrigins")]
    public class FaqController(IFaqBll FaqBll, IDcpMapper mapper) : BaseController<Faq, FaqDto, Guid, FaqFilter>(FaqBll, mapper)
    {
        public override async Task<DcpResponse<PageResult<FaqDto>>> GetAllAsync([FromBody] FaqFilter searchParameters)=> new DcpResponse<PageResult<FaqDto>>(mapper.Map<PageResult<FaqDto>>(await FaqBll.GetAllAsync(searchParameters)));
    }
}
