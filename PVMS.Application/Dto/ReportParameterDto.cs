namespace PVMS.Application.Dto
{
    public class ReportParameterDto : BaseDto<Guid>
    {
        public Guid? ReportId { get; set; }
        public string ReportName { get; set; }
        public string ParameterName { get; set; }
        public string ParameterTitle { get; set; }
        public string ParameterType { get; set; }
        public string ParameterQuery { get; set; }
        public int ParameterOrder { get; set; }

    }
}
