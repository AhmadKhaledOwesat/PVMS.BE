namespace PVMS.Application.Dto
{
    public class UserWorkFlowDefinitionDto : BaseDto<Guid>
    {
        public Guid UserId { get; set; }
        public Guid WorkFlowDefinitionId { get; set; }
    }
}
