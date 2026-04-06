
using PVMS.Domain.Entities;

namespace PVMS.Application.Dto
{
    public class WorkFlowHistoryDto : BaseDto<Guid>
    {
        public Guid TicketId { get; set; }
        public int OldStatusId { get; set; }
        public int NewStatusId { get; set; }
        public string Note { get; set; }
        public string Title { get; set; }
        public Guid? ProcedureTypeId { get; set; }
        public  ProcedureTypeDto ProcedureType { get; set; }
        public  UsersDto User { get; set; }
    }
}
