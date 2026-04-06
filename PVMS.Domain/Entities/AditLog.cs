namespace PVMS.Domain.Entities
{
    /// <summary>
    /// Audit log entity matching [dbo].[AditLog] table.
    /// Stores EntityType, EntityId, OldValue/NewValue as JSON, CreatedBy, CreatedDate.
    /// </summary>
    public class AditLog : BaseEntity<Guid>
    {
        public string? EntityType { get; set; }
        public Guid? EntityId { get; set; }
        public string? OldValue { get; set; }
        public string? NewValue { get; set; }

        public virtual Users? Creator { get; set; }
        public virtual Users? Modifier { get; set; }
    }
}
