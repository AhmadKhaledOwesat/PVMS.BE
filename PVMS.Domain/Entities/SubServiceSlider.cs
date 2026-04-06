using System.ComponentModel.DataAnnotations.Schema;

namespace DcpTracker.Domain.Entities
{
    public class SubServiceSlider : MainSlider
    {
        public Guid SubServiceId { get; set; }
        [ForeignKey(nameof(SubServiceId))]
        public virtual SubService SubService { get; set; }
    }
}
