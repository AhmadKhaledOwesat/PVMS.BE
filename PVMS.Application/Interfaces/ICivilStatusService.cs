using PVMS.Application.Dto;

namespace PVMS.Application.Interfaces
{
    public interface ICivilStatusService
    {
        Task<ArrayOfPersonInformationDto> GetPersonInformationAsync(string nationalId);
    }
}
