namespace PVMS.Domain.Entities.Filters
{
    public class CitizenFilter : SearchParameters<Citizen>
    {
        public string FullName { get; set; }
    }
}
