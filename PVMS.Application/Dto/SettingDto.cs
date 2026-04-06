namespace PVMS.Application.Dto
{
    public class SettingDto : BaseDto<Guid>
    {
        public string SettingName { get; set; }
        public string DisplayName { get; set; }
        public string SettingValue { get; set; }
        public string SettingValueOt { get; set; }
        public int IsMedia { get; set; }
        public int EnableEditor { get; set; }
        public int SendToMobileApp { get; set; }
        
        public int IsSystem { get; set; }

    }
}
