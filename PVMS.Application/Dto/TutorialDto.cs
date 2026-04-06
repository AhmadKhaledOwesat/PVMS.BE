namespace DcpTracker.Application.Dto
{
    public class TutorialDto : BaseDto<Guid>
    {
        public string DescAr { get; set; }
        public string DescOt { get; set; }
        public string TitleAr { get; set; }
        public string TitleOt { get; set; }
        public string Icon { get; set; }
        public int Active { get; set; }
    }
}
