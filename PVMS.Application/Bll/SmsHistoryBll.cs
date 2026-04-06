using PVMS.Application.Interfaces;
using PVMS.Domain.Entities;
using PVMS.Domain.Entities.Filters;
using PVMS.Domain.Interfaces;

namespace PVMS.Application.Bll
{
    public class SmsHistoryBll(IBaseDal<SmsHistory, Guid, SmsHistoryFilter> baseDal) : BaseBll<SmsHistory, Guid, SmsHistoryFilter>(baseDal), ISmsHistoryBll
    {
    }
}
