namespace PVMS.Domain.Entities.Filters
{
    public class ProcedureTypeFilter : SearchParameters<ProcedureType>
    {
        public string Description { get; set; }
        public int? Active { get; set; }

    }
}
