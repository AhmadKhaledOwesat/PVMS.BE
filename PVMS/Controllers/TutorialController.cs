using DcpTracker.Application.Dto;
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
    public class TutorialController(ITutorialBll tutorialBll,IDcpMapper mapper) : BaseController<Tutorial,TutorialDto,Guid,TutorialFilter>(tutorialBll, mapper)
    {
        public override async Task<DcpResponse<PageResult<TutorialDto>>> GetAllAsync([FromBody] TutorialFilter searchParameters)=> new DcpResponse<PageResult<TutorialDto>>(mapper.Map<PageResult<TutorialDto>>(await tutorialBll.GetAllAsync(searchParameters)));    
    }
}
