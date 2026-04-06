namespace PVMS.Domain.Entities.Filters
{
    public class ReportFilter : SearchParameters<Report>
    {
        public string Name { get; set; }
        
    }
}
