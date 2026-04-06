namespace DcpTracker.Domain.Entities
{
    public class MainSlider : BaseEntity<Guid>
    {
        public string ImageFile { get; set; }
        public int SortOrder { get; set; }
        public string CallActionTextAr { get; set; }
        public string CallActionTextOt { get; set; }
        public string DistenationType { get; set; }
        public string DistenationLink { get; set; }
        public int Active { get; set; }
    }
}
