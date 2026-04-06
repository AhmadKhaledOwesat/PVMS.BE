namespace DcpTracker.Domain.Entities
{
    public class MainService : BaseEntity<Guid>
    {
        public string DescAr { get; set; }
        public string DescOt { get; set; }
        public string Icon { get; set; }
        public int ActiveForUserAdd { get; set; }
        public int ActiveForAppDisplay { get; set; }
        public int SortOrder { get; set; }
        public virtual ICollection<MainSubServiceMapping> MainSubServiceMappings { get; set; }  

    }
}
