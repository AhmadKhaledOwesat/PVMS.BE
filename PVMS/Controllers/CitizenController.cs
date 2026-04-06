using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using PVMS.Application.Dto;
using PVMS.Application.Interfaces;
using PVMS.Domain.Entities;
using PVMS.Domain.Entities.Filters;
using PVMS.Domain.Interfaces;
using System.IO.Compression;

namespace PVMS.Controllers
{
    
    [ApiController]
    [Route("api/[controller]")]
    [EnableCors("AllowAllOrigins")]
    public class CitizenController(ICitizenBll appUserBll, IInnovaMapper mapper, ICivilStatusService civilStatusService, ICitizenImageBackfillService citizenImageBackfillService, IServiceScopeFactory scopeFactory, IOptions<AssetsOptions> assetsOptions) : BaseController<Citizen, CitizenDto, Guid, CitizenFilter>(appUserBll, mapper)
    {
        public override async Task<InnovaResponse<PageResult<CitizenDto>>> GetAllAsync([FromBody] CitizenFilter searchParameters) => new InnovaResponse<PageResult<CitizenDto>>(mapper.Map<PageResult<CitizenDto>>(await appUserBll.GetAllAsync(searchParameters)));

        [HttpGet]
        [Route("verify/{nationalId}")]
        public async Task<InnovaResponse<ArrayOfPersonInformationDto?>> VerifyAsync([FromRoute] string nationalId) => 
            new InnovaResponse<ArrayOfPersonInformationDto?>(await civilStatusService.GetPersonInformationAsync(nationalId));

        /// <summary>
        /// Backfills ImagePath for citizens that have no image (fetches from Civil Status, saves as nationalId.png via InnovaExtensions).
        /// Runs in background to avoid timeout for ~3000 records. Optional query params: batchSize (default 50), delayBetweenCallsMs (default 300).
        /// </summary>
        [HttpPost]
        [Route("backfill-images")]
        public IActionResult StartBackfillImages([FromQuery] int batchSize = 50, [FromQuery] int delayBetweenCallsMs = 300)
        {
            _ = Task.Run(async () =>
            {
                using (var scope = scopeFactory.CreateScope())
                {
                    var service = scope.ServiceProvider.GetRequiredService<ICitizenImageBackfillService>();
                    await service.BackfillCitizenImagesAsync(batchSize, delayBetweenCallsMs);
                }
            });
            return Accepted(new { message = "Citizen image backfill started. Citizens without ImagePath will be updated with nationalId.png from Civil Status." });
        }

        /// <summary>
        /// Runs backfill synchronously and returns the result. Use for smaller batches or when you need the counts (may timeout for ~3000 records).
        /// </summary>
        [HttpPost]
        [Route("backfill-images/sync")]
        public async Task<InnovaResponse<CitizenImageBackfillResult>> BackfillImagesSync([FromQuery] int batchSize = 50, [FromQuery] int delayBetweenCallsMs = 300, CancellationToken cancellationToken = default)
        {
            var result = await citizenImageBackfillService.BackfillCitizenImagesAsync(batchSize, delayBetweenCallsMs, cancellationToken);
            return new InnovaResponse<CitizenImageBackfillResult>(result);
        }

        /// <summary>
        /// Downloads all citizen images as a zip file (citizens with ImagePath only).
        /// </summary>
        [HttpGet("images/batch")]
        public async Task<IActionResult> DownloadAllImages()
        {
            const string CitizenImagesFolder = "citizens";
            var fileNames = await appUserBll.GetCitizenImageFileNamesAsync();
            var citizensRoot = Path.Combine(Path.GetDirectoryName(assetsOptions.Value.Path) ?? "", CitizenImagesFolder);
            var memory = new MemoryStream();
            using (var zip = new ZipArchive(memory, ZipArchiveMode.Create, true))
            {
                foreach (var fileName in fileNames)
                {
                    if (string.IsNullOrWhiteSpace(fileName)) continue;

                    var path = Path.Combine(citizensRoot, fileName);
                    if (!System.IO.File.Exists(path))
                        continue;

                    var entry = zip.CreateEntry(fileName);
                    using var entryStream = entry.Open();
                    using var fileStream = System.IO.File.OpenRead(path);
                    await fileStream.CopyToAsync(entryStream);
                }
            }

            memory.Position = 0;
            return File(memory, "application/zip", "citizen-images.zip");
        }
    }
}
