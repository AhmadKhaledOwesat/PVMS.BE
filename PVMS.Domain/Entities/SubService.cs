namespace DcpTracker.Domain.Entities
{
    public class SubService : BaseEntity<Guid>
    {
        public string DescAr { get; set; }
        public string DescOt { get; set; }
        public string Tags { get; set; }
        public string SubServiceIcon { get; set; }
        public string DefaultBannerImage { get; set; }
        public int ActiveForAppDisplay { get; set; }
        public int ActiveForUserAdd { get; set; }
        public int SortOrder { get; set; }
        public string Notes { get; set; }
        public virtual ICollection<ServiceRequest> ServiceRequests { get; set; }
        public virtual ICollection<MainSubServiceMapping> MainSubServiceMappings { get; set; }

    }
}
