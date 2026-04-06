using PVMS.Application.Dto;
using PVMS.Domain.Entities;
using PVMS.Domain.Entities.Filters;
using PVMS.Domain.Interfaces;

namespace PVMS.Application.Interfaces
{
    public interface ITicketBll : IBaseBll<Ticket, Guid, TicketFilter>
    {
        Task<byte[]> GenerateReportAsync(Guid id);
        Task<QrCodeDto> GetDetailsAsync(QrCodeDto entity);
    }
}
