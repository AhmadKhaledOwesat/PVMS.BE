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
    public class NationalityController(INationalityBll nationalityBll, IInnovaMapper mapper) : BaseController<Nationality,NationalityDto,Guid,NationalityFilter>(nationalityBll, mapper)
    {
        public override async Task<InnovaResponse<PageResult<NationalityDto>>> GetAllAsync([FromBody] NationalityFilter searchParameters)
        {
            return  new InnovaResponse<PageResult<NationalityDto>>(mapper.Map<PageResult<NationalityDto>>(await nationalityBll.GetAllAsync(searchParameters)));      
        }
    }
}
