using PVMS.Domain.Entities;
using PVMS.Domain.Entities.Filters;
using PVMS.Domain.Interfaces;

namespace PVMS.Application.Bll
{
    public class ReportParameterBll(IBaseDal<ReportParameter, Guid, ReportParameterFilter> baseDal) : BaseBll<ReportParameter, Guid, ReportParameterFilter>(baseDal), IReportParameterBll
    {
        public override async Task<PageResult<ReportParameter>> GetAllAsync(ReportParameterFilter searchParameters)
        {
            searchParameters.Expression = new Func<ReportParameter, bool>(a => a.ReportId == searchParameters.ReportId);
            var data = await base.GetAllAsync(searchParameters);
            data.Collections = [.. data.Collections.OrderBy(a => a.ParameterOrder)];
            return data;
        }
    }
}
