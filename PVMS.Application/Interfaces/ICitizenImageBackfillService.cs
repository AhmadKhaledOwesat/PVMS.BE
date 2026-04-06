namespace PVMS.Application.Interfaces
{
    public interface ICitizenImageBackfillService
    {
        /// <summary>
        /// Backfills ImagePath for citizens that have no image by fetching from Civil Status and saving via InnovaExtensions (fileName: nationalId.png).
        /// </summary>
        /// <param name="batchSize">Number of citizens to process per batch (default 50).</param>
        /// <param name="delayBetweenCallsMs">Delay in ms between Civil Status API calls to avoid throttling (default 300).</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Count of citizens updated with an image.</returns>
        Task<CitizenImageBackfillResult> BackfillCitizenImagesAsync(int batchSize = 50, int delayBetweenCallsMs = 300, CancellationToken cancellationToken = default);
    }

    public class CitizenImageBackfillResult
    {
        public int TotalWithoutImage { get; set; }
        public int Processed { get; set; }
        public int Saved { get; set; }
        public int SkippedNoImage { get; set; }
        public int Failed { get; set; }
    }
}
