using PVMS.Application.Interfaces;
using PVMS.Domain.Entities;
using PVMS.Domain.Entities.Filters;
using PVMS.Domain.Interfaces;

namespace PVMS.Application.Bll
{
    public class UserWorkFlowDefinitionBll(IBaseDal<UserWorkFlowDefinition, Guid, UserWorkFlowDefinitionFilter> baseDal)
        : BaseBll<UserWorkFlowDefinition, Guid, UserWorkFlowDefinitionFilter>(baseDal), IUserWorkFlowDefinitionBll
    {
        public override Task<PageResult<UserWorkFlowDefinition>> GetAllAsync(UserWorkFlowDefinitionFilter searchParameters)
        {
            searchParameters.Expression = new Func<UserWorkFlowDefinition, bool>(a => a.UserId == searchParameters.UserId);
            return base.GetAllAsync(searchParameters);
        }
    }
}
