using PVMS.Domain.Entities;
using PVMS.Domain.Entities.Filters;
using PVMS.Domain.Interfaces;

namespace PVMS.Application.Interfaces
{
    public interface ILocationBll : IBaseBll<Location, Guid, LocationFilter>
    {
    }
}
