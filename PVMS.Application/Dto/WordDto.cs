namespace PVMS.Application.Dto
{
    public class WordDto 
    {
        public string FullName { get; set; }
        public string Nationality { get; set; }
        public string NationalNo { get; set; }
        public string Gender { get; set; }
        public List<string> Types { get; set; }
        public string DayName { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public string UserName { get; set; }
        public string MobileNo { get; set; }
        public string QrCode { get; set; }
    }
    public class PlaceHolder {
    
        public bool IsList { get; set; }
        public string Key { get; set; }
        public object Value  { get; set; }
    }

}
