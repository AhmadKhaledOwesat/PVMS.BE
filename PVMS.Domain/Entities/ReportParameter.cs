using System.ComponentModel.DataAnnotations.Schema;

namespace PVMS.Domain.Entities
{
    public class ReportParameter : BaseEntity<Guid>
    {
        public Guid? ReportId { get; set; }
        [ForeignKey(nameof(ReportId))]
        public virtual Report Report { get; set; }
        public string ParameterName { get; set; }
        public string ParameterTitle { get; set; }
        public string ParameterType { get; set; }
        public string ParameterQuery { get; set; }
        public int ParameterOrder { get; set; }
    }
}
