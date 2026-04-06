using DcpTracker.Domain.Entities;
using DcpTracker.Domain.Entities.Filters;
using DcpTracker.Domain.Interfaces;

namespace DcpTracker.Application.Interfaces
{
    public interface IServiceRequestBll : IBaseBll<ServiceRequest, Guid, ServiceRequestFilter>
    {
        Task<DcpResponse<bool>> UpdateLocation(Guid requestId, string fieldName, string gps);
    }
}
