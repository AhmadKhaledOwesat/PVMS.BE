namespace PVMS.Domain.Entities.Filters
{
    public class SmsHistoryFilter : SearchParameters<SmsHistory>
    {
        public string Description { get; set; }
        public int? Active { get; set; }

    }
}
