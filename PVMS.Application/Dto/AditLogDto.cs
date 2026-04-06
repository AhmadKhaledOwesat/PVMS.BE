namespace PVMS.Application.Dto
{
    public class AditLogDto : BaseDto<Guid>
    {
        public string? EntityType { get; set; }
        public Guid? EntityId { get; set; }
        public string? OldValue { get; set; } = string.Empty;
        public string? NewValue { get; set; } = string.Empty;
        public string? CreatedByName { get; set; }
        public string? ModifiedByName { get; set; }
    }
}
