namespace PVMS.Domain
{
    /// <summary>
    /// Provides the current user id for audit logging. Implemented in Application layer (e.g. from JWT).
    /// </summary>
    public interface ICurrentUserProvider
    {
        Guid? GetCurrentUserId();
    }
}
