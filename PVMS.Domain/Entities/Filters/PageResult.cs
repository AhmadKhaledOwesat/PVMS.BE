namespace PVMS.Domain.Entities.Filters
{
    public sealed class PageResult<T>
    {
        public int Count { get; set; } = 0;
        public List<T> Collections { get; set; } = [];
    }
}
