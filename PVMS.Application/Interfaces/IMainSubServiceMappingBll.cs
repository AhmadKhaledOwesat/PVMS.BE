using DcpTracker.Domain.Entities;
using DcpTracker.Domain.Entities.Filters;
using Version = DcpTracker.Domain.Entities.Version;

namespace DcpTracker.Domain.Interfaces
{
    public interface IMainSubServiceMappingBll : IBaseBll<MainSubServiceMapping, Guid, MainSubServiceMappingFilter>
    {
    }
}
