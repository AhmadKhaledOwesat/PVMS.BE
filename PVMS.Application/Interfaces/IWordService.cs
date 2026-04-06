using PVMS.Application.Dto;

namespace PVMS.Application.Interfaces
{
    public interface IWordService
    {
        Task ReplaceTextAsync(string ticketId, List<PlaceHolder> placeHolders);
    }
}
