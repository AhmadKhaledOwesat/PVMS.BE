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
    public class TypeCategoryController(ITypeCategoryBll TypeCategoryBll,IInnovaMapper mapper) : BaseController<TypeCategory,TypeCategoryDto,Guid,TypeCategoryFilter>(TypeCategoryBll, mapper)
    {
        public override async Task<InnovaResponse<PageResult<TypeCategoryDto>>> GetAllAsync([FromBody] TypeCategoryFilter searchParameters)
        {
            return  new InnovaResponse<PageResult<TypeCategoryDto>>(mapper.Map<PageResult<TypeCategoryDto>>(await TypeCategoryBll.GetAllAsync(searchParameters)));      
        }
    }
}
