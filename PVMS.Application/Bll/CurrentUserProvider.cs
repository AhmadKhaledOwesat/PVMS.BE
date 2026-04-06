using PVMS.Application.Interfaces;
using PVMS.Domain;

namespace PVMS.Application.Bll
{
    public class CurrentUserProvider(IIdentityManager<Guid> identityManager) : ICurrentUserProvider
    {
        public Guid? GetCurrentUserId()
        {
            var id = identityManager.CurrentUserId;
            return id.Equals(Guid.Empty) ? null : id;
        }
    }
}
