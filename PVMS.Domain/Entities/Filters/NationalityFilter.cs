namespace PVMS.Domain.Entities.Filters
{
    public class NationalityFilter : SearchParameters<Nationality>
    {
        public string Description { get; set; }
        public int? Active { get; set; }

    }
}
