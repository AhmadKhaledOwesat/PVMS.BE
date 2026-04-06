using PVMS.Domain.Enum;

namespace PVMS.Domain.Entities
{
    public class SmsHistory : BaseEntity<Guid>
    {
        public string Message { get; set; }
        public string Note { get; set; }
        public Guid? UserId { get; set; }
        public int? IsVerified { get; set; }
        public SmsType? TypeId { get; set; }

    }
}
