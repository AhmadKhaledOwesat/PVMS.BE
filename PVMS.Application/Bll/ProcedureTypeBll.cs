using PVMS.Application.Interfaces;
using PVMS.Domain.Entities;
using PVMS.Domain.Entities.Filters;
using PVMS.Domain.Interfaces;

namespace PVMS.Application.Bll
{
    public class ProcedureTypeBll(IBaseDal<ProcedureType, Guid, ProcedureTypeFilter> baseDal) : BaseBll<ProcedureType, Guid, ProcedureTypeFilter>(baseDal), IProcedureTypeBll
    {
        public override Task<PageResult<ProcedureType>> GetAllAsync(ProcedureTypeFilter searchParameters)
        {
            if (!string.IsNullOrEmpty(searchParameters.Description))
                searchParameters.Expression = new Func<ProcedureType, bool>(a => a.NameAr == searchParameters?.Description && (searchParameters.Active == null || a.Active == searchParameters.Active));
            return base.GetAllAsync(searchParameters);
        }

    }
}
