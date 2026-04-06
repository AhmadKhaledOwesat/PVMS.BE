namespace PVMS.Application.Dto
{
    public class SmsHistoryDto : BaseDto<Guid>
    {
        public string Code { get; set; }
        public string Note { get; set; }
        public Guid? UserId { get; set; }
        public int? IsVerified { get; set; }
    }
}
