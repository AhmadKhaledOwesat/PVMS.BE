using Microsoft.Reporting.NETCore;
using PVMS.Application.Interfaces;
using PVMS.Infrastructure.Extensions;
using ReportParameter = Microsoft.Reporting.NETCore.ReportParameter;

namespace PVMS.Application.Services
{
    public class ReportService : IReportService
    {
        public byte[] GeneratePdfAsync<T>(string reportPath,string dataSetName,IEnumerable<T> data,Dictionary<string, string> parameters = null)
            {
            var report = new LocalReport
            {
                ReportPath = reportPath
            };

            if (!dataSetName.IsNullOrEmpty())
                report.DataSources.Add(new ReportDataSource(dataSetName, data));

            report.EnableExternalImages = true;
            if (parameters != null)
            {
                report.SetParameters(parameters.Select(p => new ReportParameter(p.Key, p.Value)));
            }

            return report.Render(
                "PDF",
                null,
                out _,
                out _,
                out _,
                out _,
                out _
            );
        }
    }
}
