using PVMS.Application.Dto;
using PVMS.Domain.Entities;
using PVMS.Domain.Entities.Filters;
using PVMS.Domain.Interfaces;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using PVMS.Application.Interfaces;
using PVMS.Application.Bll;

namespace PVMS.Controllers
{
    
    [ApiController]
    [Route("api/[controller]")]
    [EnableCors("AllowAllOrigins")]
    public class FileUploadController(IFileUploderBll fileUploderBll)
    {
        [HttpPost("UploadChunk")]
        public async Task<FileResultDto> UploadChunk([FromForm] IFormFile chunk,[FromForm] string fileName,[FromForm] long offset,[FromForm] long totalSize, [FromForm] Guid ticketId)
        {
            return await fileUploderBll.UploadChunkAsync(chunk, fileName, offset, totalSize, ticketId);
        }
    }
}
