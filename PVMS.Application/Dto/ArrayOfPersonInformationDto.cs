using System.Xml.Serialization;

namespace PVMS.Application.Dto
{
  
    [XmlRoot("ArrayOfPersonInformation",
        Namespace = "http://schemas.datacontract.org/2004/07/WebApplication_New_CV.App_Start")]
    public class ArrayOfPersonInformationDto
    {
        [XmlElement("PersonInformation")]
        public List<PersonInformationDto> Persons { get; set; } = [];
    }
}
