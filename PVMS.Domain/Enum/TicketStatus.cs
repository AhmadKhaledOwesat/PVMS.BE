namespace PVMS.Domain.Enum
{
    public enum TicketStatus
    {
        New = 1,
        UnderReview = 2,
        UnderVerification = 3,
        LegalApproval = 4,
        BactToSite = -1,
        SentToCourt = 5,
        Archived = 6,
    }
}
