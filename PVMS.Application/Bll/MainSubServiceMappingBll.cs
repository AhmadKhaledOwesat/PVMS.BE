using DcpTracker.Domain.Entities;
using DcpTracker.Domain.Entities.Filters;
using DcpTracker.Domain.Interfaces;

namespace DcpTracker.Application.Bll
{
    public class MainSubServiceMappingBll(IBaseDal<MainSubServiceMapping, Guid, MainSubServiceMappingFilter> baseDal) : BaseBll<MainSubServiceMapping, Guid, MainSubServiceMappingFilter>(baseDal), IMainSubServiceMappingBll
    {
    }
}
