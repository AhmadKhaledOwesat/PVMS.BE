using PVMS.Application.Interfaces;
using PVMS.Domain.Entities;
using PVMS.Domain.Entities.Filters;
using PVMS.Domain.Interfaces;
using PVMS.Infrastructure.Extensions;

namespace PVMS.Application.Bll
{
#pragma warning disable CS9107 // Parameter is captured into the state of the enclosing type and its value is also passed to the base constructor. The value might be captured by the base class as well.
    public class ReportBll(IBaseDal<Report, Guid, ReportFilter> baseDal) : BaseBll<Report, Guid, ReportFilter>(baseDal), IReportBll
#pragma warning restore CS9107 // Parameter is captured into the state of the enclosing type and its value is also passed to the base constructor. The value might be captured by the base class as well.
    {
        public override Task<PageResult<Report>> GetAllAsync(ReportFilter searchParameters)
        {
            if (searchParameters is not null)
            {
                    searchParameters.Expression = new Func<Report, bool>(a =>
                    (searchParameters.Keyword.IsNullOrEmpty() || a.ReportName.Contains(searchParameters?.Keyword))
                    );
            }

            return base.GetAllAsync(searchParameters);
        }

        public async Task<InnovaResponse<dynamic>> ExecuteReport(string query)
        {
            dynamic data = await baseDal.ExecuteSQL(query);
            return new InnovaResponse<dynamic>(data);
        }
    }
}
