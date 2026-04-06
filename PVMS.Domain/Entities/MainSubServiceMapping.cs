using System.ComponentModel.DataAnnotations.Schema;

namespace DcpTracker.Domain.Entities
{
    public class MainSubServiceMapping : BaseEntity<Guid>
    {
        public Guid? MainServiceId { get; set; }
        [ForeignKey(nameof(MainServiceId))]
        public virtual MainService MainService { get; set; }

        public Guid? SubServiceId { get; set; }
        [ForeignKey(nameof(SubServiceId))]
        public virtual SubService SubService { get; set; }
    }
}
