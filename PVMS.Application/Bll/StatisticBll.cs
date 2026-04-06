using PVMS.Domain.Entities;
using PVMS.Domain.Entities.Filters;
using PVMS.Domain.Interfaces;

namespace PVMS.Application.Bll
{
#pragma warning disable CS9107 // Parameter is captured into the state of the enclosing type and its value is also passed to the base constructor. The value might be captured by the base class as well.
    public class StatisticBll(IBaseDal<Statistic, Guid, StatisticFilter> baseDal) : BaseBll<Statistic, Guid, StatisticFilter>(baseDal), IStatisticBll
#pragma warning restore CS9107 // Parameter is captured into the state of the enclosing type and its value is also passed to the base constructor. The value might be captured by the base class as well.
    {
        public override async Task<PageResult<Statistic>> GetAllAsync(StatisticFilter searchParameters)
        {

            var data = await base.GetAllAsync(searchParameters);

            foreach (var item in data.Collections)
            {
                item.Result = await baseDal.ExecuteSQL(item.Query);
                item.ResultOt = await baseDal.ExecuteSQL(item.QueryOt);
            }
            data.Collections = [.. data.Collections.OrderBy(a => a.SortOrder)];
            return data;
        }
    }
}
