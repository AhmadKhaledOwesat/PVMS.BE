using PVMS.Domain.Entities;

namespace PVMS.Application.Dto
{
    public class TicketTypeDto : BaseDto<Guid>
    {
        public string NameAr { get; set; }
        public string NameOt { get; set; }
        public int Active { get; set; }
        public string Notes { get; set; }
        public List<TicketTypePriceDto> TicketTypePrices { get; set; } = [];
        public  ICollection<TypeCategoryDto> TypeCategories { get; set; }


    }
}
