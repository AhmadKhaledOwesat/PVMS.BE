using PVMS.Application.Dto;
using PVMS.Application.Interfaces;
using PVMS.Domain.Entities;
using PVMS.Infrastructure.Extensions;

namespace PVMS.Application.Services
{
    public class CitizenImageBackfillService(ICitizenBll citizenBll, ICivilStatusService civilStatusService) : ICitizenImageBackfillService
    {
        private const string CitizenImagesFolder = "citizens";

        public async Task<CitizenImageBackfillResult> BackfillCitizenImagesAsync(int batchSize = 50, int delayBetweenCallsMs = 300, CancellationToken cancellationToken = default)
        {
            var result = new CitizenImageBackfillResult();
            var citizens = await citizenBll.FindAllByExpressionAsync(c =>
                c.NationalId != null && (string.IsNullOrEmpty(c.ImagePath)));

            result.TotalWithoutImage = citizens.Count;
            if (citizens.Count == 0)
                return result;

            for (var i = 0; i < citizens.Count && !cancellationToken.IsCancellationRequested; i++)
            {
                var citizen = citizens[i];
                try
                {
                    var personInfo = await civilStatusService.GetPersonInformationAsync(citizen.NationalId);
                    var person = personInfo?.Persons?.FirstOrDefault();
                    var base64Image = person?.GetPersonalImageResult;

                    if (string.IsNullOrWhiteSpace(base64Image))
                    {
                        result.SkippedNoImage++;
                        result.Processed++;
                        await Task.Delay(delayBetweenCallsMs, cancellationToken);
                        continue;
                    }

                    var fileName = await base64Image.UplodaFiles(".png", CitizenImagesFolder, citizen.NationalId);
                    citizen.ImagePath = fileName;
                    await citizenBll.UpdateAsync(citizen);
                    result.Saved++;
                }
                catch
                {
                    result.Failed++;
                }

                result.Processed++;
                if (delayBetweenCallsMs > 0)
                    await Task.Delay(delayBetweenCallsMs, cancellationToken);
            }

            return result;
        }
    }
}
