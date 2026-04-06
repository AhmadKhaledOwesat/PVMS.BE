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
    public class UserController(IUserBll userBll,IInnovaMapper mapper) : BaseController<Users,UsersDto,Guid,UserFilter>(userBll, mapper)
    {
        public override async Task<InnovaResponse<PageResult<UsersDto>>> GetAllAsync([FromBody] UserFilter searchParameters)=> new InnovaResponse<PageResult<UsersDto>>(mapper.Map<PageResult<UsersDto>>(await userBll.GetAllAsync(searchParameters)));
        [HttpGet]
        [Route("login/{userName}/{password}")]
        public async Task<InnovaResponse<UsersDto>> LoginAsync(string userName, string password) => await userBll.LoginAsync(userName,  password);

        [HttpGet]
        [Route("reset-password/{userName}")]
        public async Task<InnovaResponse<string>> ResetPasswordAsync(string userName) => await userBll.ResetPasswordAsync(userName);

        [HttpGet]
        [Route("update-password/{userName}/{newPassword}")]
        public async Task<InnovaResponse<string>> UpdatePasswordAsync(string userName, string newPassword) => await userBll.UpdatePasswordAsync(userName, newPassword);

        [HttpGet]
        [Route("update-location/{id}/{location}")]
        public async Task<InnovaResponse<bool>> UpdateLocationAsync(Guid id, string location) => await userBll.UpdateLocationAsync(id, location);



    }
}
