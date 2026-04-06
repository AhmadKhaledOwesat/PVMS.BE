namespace DcpTracker.Application.Dto
{
    public class MainServiceDto : BaseDto<Guid>
    {
        public string DescAr { get; set; }
        public string DescOt { get; set; }
        public string Icon { get; set; }
        public int ActiveForUserAdd { get; set; }
        public int ActiveForAppDisplay { get; set; }
        public int SortOrder { get; set; }
        public virtual ICollection<MainSubServiceMappingDto> MainSubServiceMappings { get; set; }

    }
}
