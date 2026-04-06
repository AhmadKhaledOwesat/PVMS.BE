namespace DcpTracker.Application.Dto
{
    public class GovernorateDto : BaseDto<Guid>
    {
        public string DescAr { get; set; }
        public string DescOt { get; set; }
        public int SortOrder { get; set; }
        public int Active { get; set; }
        public Guid? CountryId { get; set; }
        public string CountryName { get; set; }
        public virtual ICollection<TransactionDto> Cities { get; set; }

    }
}
