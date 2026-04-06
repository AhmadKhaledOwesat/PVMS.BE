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
    public class LocationController(ILocationBll TaskStatusBll,IInnovaMapper mapper) : BaseController<Location,LocationDto,Guid,LocationFilter>(TaskStatusBll, mapper)
    {
        public override async Task<InnovaResponse<PageResult<LocationDto>>> GetAllAsync([FromBody] LocationFilter searchParameters)
        {
            return  new InnovaResponse<PageResult<LocationDto>>(mapper.Map<PageResult<LocationDto>>(await TaskStatusBll.GetAllAsync(searchParameters)));      
        }
    }
}
