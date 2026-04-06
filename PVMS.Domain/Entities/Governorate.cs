using System.ComponentModel.DataAnnotations.Schema;

namespace DcpTracker.Domain.Entities
{
    public class Governorate : BaseEntity<Guid>
    {
        public string DescAr { get; set; }
        public string DescOt { get; set; }
        public int SortOrder { get; set; }
        public int Active { get; set; }
        public Guid? CountryId { get; set; }
        [ForeignKey(nameof(CountryId))]
        public virtual Company Country { get; set; }
        public virtual ICollection<Transaction> Cities { get; set; }
    }
}
