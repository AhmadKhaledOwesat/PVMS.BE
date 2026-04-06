using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using PVMS.Application.Dto;
using PVMS.Application.Interfaces;

namespace PVMS.Application.Bll
{
    public class FileUploderBll(IOptions<AssetsOptions> configuration) : IFileUploderBll
    {
        public async Task<FileResultDto> UploadChunkAsync(IFormFile chunk, string fileName, long offset, long totalSize, Guid ticketId)
        {
            var uploadFolder = Path.Combine(Path.Combine(configuration.Value.Path, ticketId.ToString()), "uploads");

            if (!Directory.Exists(uploadFolder))
                Directory.CreateDirectory(uploadFolder);

            var filePath = Path.Combine(uploadFolder, fileName);

            // Append chunk to file
            using (var stream = new FileStream(filePath, offset == 0 ? FileMode.Create : FileMode.Append))
            {
                await chunk.CopyToAsync(stream);
            }

            // Optionally, return progress
            var currentSize = new FileInfo(filePath).Length;
            return new FileResultDto { Uploaded = currentSize, TotalSize = totalSize, Complete = currentSize == totalSize };
        }


    }

    public class FileResultDto
    {
        public long Uploaded { get; set; }
        public string FileName { get; set; }
        public long TotalSize { get; set; }
        public bool Complete { get; set; }
    }

}
