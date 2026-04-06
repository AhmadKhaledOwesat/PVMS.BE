using System.Xml.Serialization;
namespace PVMS.Application.Dto
{

    public class PersonInformationDto
    {
        [XmlElement("ArabicName")]
        public string ArabicName { get; set; }

        [XmlElement("CivilQualifierInfo", IsNullable = true)]
        public string CivilQualifierInfo { get; set; }

        [XmlElement("CountryOfBirth")]
        public string CountryOfBirth { get; set; }

        [XmlElement("DateOfBirth", DataType = "date")]
        public DateTime DateOfBirth { get; set; }

        [XmlElement("EnglishName")]
        public string EnglishName { get; set; }

        [XmlElement("FamilyBookNum")]
        public string FamilyBookNum { get; set; }

        [XmlElement("FatherNatNo")]
        public string FatherNatNo { get; set; }

        [XmlElement("Gender")]
        public string Gender { get; set; }

        [XmlElement("PersonalCardInfo")]
        public string PersonalCardInfo { get; set; }

        [XmlElement("GetPersonalImageResult")]
        public string GetPersonalImageResult { get; set; }

        [XmlElement("NationalNumber")]
        public string NationalNumber { get; set; }

        [XmlElement("Nationality")]
        public string Nationality { get; set; }
    }
}
