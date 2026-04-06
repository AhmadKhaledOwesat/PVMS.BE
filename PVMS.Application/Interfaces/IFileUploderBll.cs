using Microsoft.AspNetCore.Http;
using PVMS.Application.Bll;

namespace PVMS.Application.Interfaces
{
    public interface IFileUploderBll
    {
        Task<FileResultDto> UploadChunkAsync(IFormFile chunk, string fileName, long offset, long totalSize, Guid ticketId);
    }
}
