namespace PVMS.Domain.Entities
{
    public class BaseEntity<TId> where TId: struct
    {
        public TId Id { get; set; }
        public TId CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public TId? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
