
namespace PVMS.Application.Dto
{
    public class ReportDto : BaseDto<Guid>
    {
        public string ReportName { get; set; }
        public string ReportProcedure { get; set; }
        public int? CategoryId { get; set; }
        public ICollection<ReportParameterDto> ReportParameters { get; set; } = [];
    }
}
