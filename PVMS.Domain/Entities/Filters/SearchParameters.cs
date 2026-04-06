using System.Text.Json.Serialization;

namespace PVMS.Domain.Entities.Filters
{
    public class SearchParameters<T>
    {
        public PagingParameters PagingParameters { get; set; }
        [JsonIgnore]
        public Func<T, bool> Expression { get; set; }

        public string Keyword { get; set; }= string.Empty;
    }
}
