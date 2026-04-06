using PVMS.Domain.Entities;
using PVMS.Domain.Entities.Filters;
using PVMS.Domain.Interfaces;

namespace PVMS.Application.Interfaces
{
    public interface ICitizenBll : IBaseBll<Citizen, Guid, CitizenFilter>
    {
        Task<List<string>> GetCitizenImageFileNamesAsync();
    }
}
