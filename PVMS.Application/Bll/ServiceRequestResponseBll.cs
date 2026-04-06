using DcpTracker.Domain.Entities;
using DcpTracker.Domain.Entities.Filters;
using DcpTracker.Domain.Interfaces;

namespace DcpTracker.Application.Bll
{
    public class ServiceRequestResponseBll(IBaseDal<ServiceRequestResponse, Guid, ServiceRequestResponseFilter> baseDal) : BaseBll<ServiceRequestResponse, Guid, ServiceRequestResponseFilter>(baseDal), IServiceRequestResponseBll
    {
    }
}
