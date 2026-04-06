namespace PVMS.Application.Dto
{
    public class TypeCategoryDto : BaseDto<Guid>
    {
        public Guid TicketTyped { get; set; }
        public Guid TicketCategoryId { get; set; }
    }
}
