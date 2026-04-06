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
    public class NotificationController(INotificationBll NotificationBll,IInnovaMapper mapper) : BaseController<Notifications,NotificationDto,Guid,NotificationFilter>(NotificationBll, mapper)
    {
        [HttpPost]
        [Route("search")]
        public override async Task<InnovaResponse<PageResult<NotificationDto>>> GetAllAsync([FromBody] NotificationFilter searchParameters)
        {
            return  new InnovaResponse<PageResult<NotificationDto>>(mapper.Map<PageResult<NotificationDto>>(await NotificationBll.GetAllAsync(searchParameters)));      
        }

        [HttpGet]
        [Route("read/{id}")]
        public async Task<InnovaResponse<bool>> ReadNotificationAsync([FromRoute] Guid id)
        {
            return new InnovaResponse<bool>(await NotificationBll.ReadNotificationAsync(id));
        }
    }
}
