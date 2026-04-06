namespace PVMS.Application.Dto
{
    public class StatisticDto : BaseDto<Guid>
    {
        public string Name { get; set; }
        public string CardDescription { get; set; }
        public string NameOt { get; set; }
        public string CardDescriptionOt { get; set; }
        public string Type { get; set; }
        public int Active { get; set; }
        public string Query { get; set; }
        public string QueryOt { get; set; }
        public int SortOrder { get; set; }
        public string Icon { get; set; }
        public string ChartColors { get; set; }
        public dynamic Result { get; set; }
        public dynamic ResultOt { get; set; }
        
    }
}
