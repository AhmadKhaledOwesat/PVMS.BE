using DcpTracker.Domain.Entities;
using DcpTracker.Domain.Entities.Filters;

namespace DcpTracker.Domain.Interfaces
{
    public interface IRateBll : IBaseBll<Rate, Guid, RateFilter>
    {
    }
}
