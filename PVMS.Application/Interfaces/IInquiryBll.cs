using DcpTracker.Domain.Entities;
using DcpTracker.Domain.Entities.Filters;

namespace DcpTracker.Domain.Interfaces
{
    public interface IInquiryBll : IBaseBll<Inquiry, Guid, InquiryFilter>
    {
        Task<Guid> MoveToAppUser(Guid id);
    }
}
