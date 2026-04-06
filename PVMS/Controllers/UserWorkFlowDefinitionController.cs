using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using PVMS.Application.Dto;
using PVMS.Application.Interfaces;
using PVMS.Domain.Entities;
using PVMS.Domain.Entities.Filters;
using PVMS.Domain.Interfaces;

namespace PVMS.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [EnableCors("AllowAllOrigins")]
    public class UserWorkFlowDefinitionController(IUserWorkFlowDefinitionBll bll, IInnovaMapper mapper)
        : BaseController<UserWorkFlowDefinition, UserWorkFlowDefinitionDto, Guid, UserWorkFlowDefinitionFilter>(bll, mapper)
    {
        public override async Task<InnovaResponse<PageResult<UserWorkFlowDefinitionDto>>> GetAllAsync([FromBody] UserWorkFlowDefinitionFilter searchParameters)
        {
            return new InnovaResponse<PageResult<UserWorkFlowDefinitionDto>>(mapper.Map<PageResult<UserWorkFlowDefinitionDto>>(await bll.GetAllAsync(searchParameters)));
        }
    }
}
