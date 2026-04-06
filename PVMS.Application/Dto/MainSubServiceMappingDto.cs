namespace DcpTracker.Application.Dto
{
    public class MainSubServiceMappingDto : BaseDto<Guid>
    {
        public Guid? MainServiceId { get; set; }
        public string MainServiceName { get; set; }
        public Guid? SubServiceId { get; set; }
        public string SubServiceName { get; set; }
    }
}
