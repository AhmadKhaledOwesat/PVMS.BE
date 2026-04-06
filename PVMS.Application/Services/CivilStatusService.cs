using Microsoft.Extensions.Options;
using PVMS.Application.Dto;
using PVMS.Application.Interfaces;
using System.Xml.Serialization;

namespace PVMS.Application.Services
{
    public class CivilStatusService(IOptions<CivilStatusOptions> options, HttpClient httpClient) : ICivilStatusService
    {
        public async Task<ArrayOfPersonInformationDto> GetPersonInformationAsync(string nationalId)
        {

            string baseUrl = string.Format(options.Value.BaseUrl, nationalId);

            var request = new HttpRequestMessage(HttpMethod.Get, baseUrl);

            request.Headers.Add("Accept", "application/xml");

            var response = await httpClient.SendAsync(request);

            response.EnsureSuccessStatusCode();

            var xml = await response.Content.ReadAsStringAsync();

            var serializer = new XmlSerializer(typeof(ArrayOfPersonInformationDto));

            using var reader = new StringReader(xml);

            return serializer.Deserialize(reader) as ArrayOfPersonInformationDto;
        }
    }
}
