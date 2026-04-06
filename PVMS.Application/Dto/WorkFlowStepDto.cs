namespace PVMS.Application.Dto
{
    public class WorkFlowStepDto
    {
        public Guid? Id { get; set; }
        /// <summary>1-based order; server reassigns from list order on save.</summary>
        public int Order { get; set; }
        public string NameAr { get; set; }
        public string NameOt { get; set; }
        public List<Guid> ApproveRoleIds { get; set; } = [];
        public List<Guid> RejectRoleIds { get; set; } = [];
        public List<Guid> SkipRoleIds { get; set; } = [];
        public bool RequireNote { get; set; }
        public string NotePrompt { get; set; }
    }
}
