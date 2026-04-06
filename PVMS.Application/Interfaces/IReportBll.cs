using PVMS.Domain.Entities;
using PVMS.Domain.Entities.Filters;
using PVMS.Domain.Interfaces;

namespace PVMS.Application.Interfaces
{
    public interface IReportBll : IBaseBll<Report, Guid, ReportFilter>
    {
        Task<InnovaResponse<dynamic>> ExecuteReport(string query);
    }
}
