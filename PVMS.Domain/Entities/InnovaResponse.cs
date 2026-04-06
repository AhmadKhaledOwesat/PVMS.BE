namespace PVMS.Domain.Entities
{
    public record InnovaResponse<T>(T Data , string Message = "" , bool IsSuccess=true);
}
