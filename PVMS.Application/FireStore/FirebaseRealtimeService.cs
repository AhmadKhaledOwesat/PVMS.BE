namespace PVMS.Application.FireStore
{
    public class FirebaseRealtimeService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl = "https://mdmapp-4bc4a-default-rtdb.firebaseio.com/";

        public FirebaseRealtimeService()
        {
            _httpClient = new HttpClient();
        }

        public async Task<string> GetDataAsync()
        {
            string node = "status.json"; // example: "users.json"
            string jsonData = string.Empty;
            HttpResponseMessage response = await _httpClient.GetAsync(_baseUrl + node);

            if (response.IsSuccessStatusCode)
            {
                jsonData = await response.Content.ReadAsStringAsync();
                return jsonData;
            }
            else
            {
                Console.WriteLine($"Error: {response.StatusCode}");
                return jsonData;
            }
        }
    }
}
