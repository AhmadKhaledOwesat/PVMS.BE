namespace PVMS.Domain.Entities.Filters
{
    public class StatisticFilter : SearchParameters<Statistic>
    {
        public int? Active { get; set; }
            

    }
}
