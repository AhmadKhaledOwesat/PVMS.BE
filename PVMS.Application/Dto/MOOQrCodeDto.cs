using Newtonsoft.Json;

namespace PVMS.Application.Dto
{
    public class MOOQrCodeDto
    {
        [JsonProperty("qrCode")]
        public string QrCode { get; set; }
    }
}
