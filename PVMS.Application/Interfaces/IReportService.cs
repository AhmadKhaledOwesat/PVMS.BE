using PVMS.Application.Dto;

namespace PVMS.Application.Interfaces
{
    public interface IReportService
    {
         byte[] GeneratePdfAsync<T>(
               string reportPath,
               string dataSetName,
               IEnumerable<T> data,
               Dictionary<string, string> parameters = null);
    
    }
}
