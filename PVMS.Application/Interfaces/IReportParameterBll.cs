using PVMS.Application.Dto;
using PVMS.Domain.Entities;
using PVMS.Domain.Entities.Filters;

namespace PVMS.Domain.Interfaces
{
    public interface IReportParameterBll : IBaseBll<ReportParameter, Guid, ReportParameterFilter>
    {
    }
}
