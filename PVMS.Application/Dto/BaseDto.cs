using System.Text.Json.Serialization;

namespace PVMS.Application.Dto
{
    public class BaseDto<TId> where TId : struct
    {
        public TId Id { get; set; }
        public TId CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public TId? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
