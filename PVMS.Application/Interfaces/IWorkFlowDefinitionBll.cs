using PVMS.Application.Dto;
using PVMS.Domain.Entities;
using PVMS.Domain.Entities.Filters;
using PVMS.Domain.Interfaces;

namespace PVMS.Application.Interfaces
{
    public interface IWorkFlowDefinitionBll : IBaseBll<WorkFlowDefinition, Guid, WorkFlowDefinitionFilter>
    {
        Task<(bool ok, string message, Guid id)> SaveNewAsync(WorkFlowDefinitionDto dto);
        Task<(bool ok, string message, Guid id)> SaveExistingAsync(WorkFlowDefinitionDto dto);
        Task<WorkFlowDefinitionDto?> GetDetailDtoByIdAsync(Guid id);
        Task<PageResult<WorkFlowDefinitionDto>> GetPagedDtosAsync(WorkFlowDefinitionFilter filter);
        Task<List<WorkFlowDefinitionMobileDto>> GetMobileAsync();
    }
}
