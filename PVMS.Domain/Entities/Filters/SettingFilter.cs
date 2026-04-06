namespace PVMS.Domain.Entities.Filters
{
    public class SettingFilter : SearchParameters<Setting>
    {
        public string Term { get; set; }
        
    }
}
