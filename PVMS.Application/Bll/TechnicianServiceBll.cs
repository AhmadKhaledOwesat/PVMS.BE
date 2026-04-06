using DcpTracker.Domain.Entities;
using DcpTracker.Domain.Entities.Filters;
using DcpTracker.Domain.Interfaces;

namespace DcpTracker.Application.Bll
{
    public class TechnicianServiceBll(IBaseDal<TechnicianService, Guid, TechnicianServiceFilter> baseDal) : BaseBll<TechnicianService, Guid, TechnicianServiceFilter>(baseDal), ITechnicianServiceBll
    {
    }
}
