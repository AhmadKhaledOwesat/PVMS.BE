namespace PVMS.Domain.Entities.Filters
{
    public class WorkFlowDefinitionFilter : SearchParameters<WorkFlowDefinition>
    {
        public int? Active { get; set; }
    }
}
