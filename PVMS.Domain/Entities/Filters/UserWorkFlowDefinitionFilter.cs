namespace PVMS.Domain.Entities.Filters
{
    public class UserWorkFlowDefinitionFilter : SearchParameters<UserWorkFlowDefinition>
    {
        public Guid UserId { get; set; }
    }
}
