using PVMS.Application.Dto;
using PVMS.Domain.Entities;
using PVMS.Domain.Entities.Filters;
using PVMS.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PVMS.Controllers
{
   // [Authorize]
    public class BaseController<T, TDto, TId, TFilter>(IBaseBll<T, TId, TFilter> _baseBll, IInnovaMapper _mapper) : Controller
        where T : BaseEntity<TId>
        where TDto : BaseDto<TId>
        where TId : struct
    {
        protected IInnovaMapper mapper = _mapper;
        protected IBaseBll<T, TId, TFilter> baseBll = _baseBll;

        [HttpPost]
        [Route("")]
        [DisableRequestSizeLimit]
        public virtual async Task<InnovaResponse<TId>> AddAsync([FromBody] TDto dto)
        {
            T entity = mapper.Map<T>(dto);
            await baseBll.AddAsync(entity);
            return new InnovaResponse<TId>(entity.Id);
        }

        [HttpPost]
        [Route("range")]
        [DisableRequestSizeLimit]
        public virtual async Task<InnovaResponse<List<TId>>> AddRangeAsync([FromBody] List<TDto> dtos)
        {
            List<T> entities = mapper.Map<List<T>>(dtos);
            await baseBll.AddRangeAsync(entities);
            return new InnovaResponse<List<TId>>([.. entities.Select(a=>a.Id)]);
        }

        [HttpPost]
        [Route("update")]
        [DisableRequestSizeLimit]
        public virtual async Task<InnovaResponse<TId>> UpdateAsync([FromBody] TDto dto)
        {
            T entity = mapper.Map<T>(dto);
            await baseBll.UpdateAsync(entity);
            return new InnovaResponse<TId>(entity.Id);
        }
        [HttpGet]
        [Route("delete/{id}")]
        public virtual async Task<InnovaResponse<bool>> DeletAsync([FromRoute] TId id) => new InnovaResponse<bool>(await baseBll.DeleteAsync(id));
        [HttpGet]
        [Route("{id}")]
        public virtual async Task<InnovaResponse<TDto>> GetByIdAsync([FromRoute] TId id) => new InnovaResponse<TDto>(mapper.Map<TDto>(await baseBll.GetByIdAsync(id)));

        [HttpPost]
        [Route("search")]
        public virtual async Task<InnovaResponse<PageResult<TDto>>> GetAllAsync([FromBody] TFilter searchParameters) => new InnovaResponse<PageResult<TDto>>(mapper.Map<PageResult<TDto>>(await baseBll.GetAllAsync(searchParameters)));
    }
}
