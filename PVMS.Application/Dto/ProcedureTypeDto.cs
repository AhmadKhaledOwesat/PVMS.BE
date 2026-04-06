namespace PVMS.Application.Dto
{
    public class ProcedureTypeDto : BaseDto<Guid>
    {
        public string NameAr { get; set; }
        public string NameOt { get; set; }
        public int Active { get; set; }
    }
}
