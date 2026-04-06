using System.ComponentModel.DataAnnotations.Schema;

namespace PVMS.Domain.Entities
{
    public class Statistic : BaseEntity<Guid>
    {
        public string Name { get; set; }
        public string CardDescription { get; set; }
        public string NameOt { get; set; }
        public string CardDescriptionOt { get; set; }
        public string Type { get; set; }
        public int Active { get; set; }
        public int SortOrder { get; set; }
        public string Query { get; set; }
        public string QueryOt { get; set; }
        public string Icon { get; set; }
        public string ChartColors { get; set; }

        [NotMapped]
        public dynamic Result { get; set; }
        [NotMapped]
        public dynamic ResultOt { get; set; }
    }
}
