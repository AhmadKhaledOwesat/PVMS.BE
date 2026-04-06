namespace PVMS.Application.Dto
{
    public class TicketCategoryDto : BaseDto<Guid>
    {
        public string NameAr { get; set; }
        public string NameOt { get; set; }
        public int Active { get; set; }

    }
}
