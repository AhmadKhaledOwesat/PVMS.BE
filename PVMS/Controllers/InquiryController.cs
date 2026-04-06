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
    public class InquiryController(IInquiryBll inquiryBll, IDcpMapper mapper) : BaseController<Inquiry, InquiryDto, Guid, InquiryFilter>(inquiryBll, mapper)
    {
        public override async Task<DcpResponse<PageResult<InquiryDto>>> GetAllAsync([FromBody] InquiryFilter searchParameters)=> new DcpResponse<PageResult<InquiryDto>>(mapper.Map<PageResult<InquiryDto>>(await inquiryBll.GetAllAsync(searchParameters)));

        [HttpGet]
        [Route("move/{id}")]
        public async Task<DcpResponse<Guid>> MoveToAppUserAsync([FromRoute] Guid id)
        {
            var appUserId = await inquiryBll.MoveToAppUser(id);
            return new DcpResponse<Guid>(appUserId, IsSuccess:true);
        }

    }
}
