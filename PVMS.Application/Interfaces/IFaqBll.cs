using DcpTracker.Domain.Entities;
using DcpTracker.Domain.Entities.Filters;
using DcpTracker.Domain.Interfaces;

namespace DcpTracker.Application.Interfaces
{
    public interface IFaqBll : IBaseBll<Faq, Guid, FaqFilter>
    {
    }
}
