namespace PVMS.Domain.Entities.Filters
{
    public class ReportParameterFilter : SearchParameters<ReportParameter>
    {
        public Guid ReportId { get; set; }
        public string Name { get; set; }
    }
}
