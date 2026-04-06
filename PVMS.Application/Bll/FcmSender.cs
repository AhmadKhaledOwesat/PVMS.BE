using Google.Apis.Auth.OAuth2;
using Google.Apis.FirebaseCloudMessaging.v1;
using Google.Apis.FirebaseCloudMessaging.v1.Data;
using Google.Apis.Services;

namespace PVMS.Application.Bll
{
    public class FcmSender
    {
        private readonly FirebaseCloudMessagingService _fcmService;
        private readonly string _projectId;

        public FcmSender(string serviceAccountPath, string projectId)
        {
            _projectId = projectId;

            GoogleCredential credential;
            using (var stream = new FileStream(serviceAccountPath, FileMode.Open, FileAccess.Read))
            {
                credential = GoogleCredential.FromStream(stream)
                    .CreateScoped("https://www.googleapis.com/auth/firebase.messaging");
            }

            _fcmService = new FirebaseCloudMessagingService(new BaseClientService.Initializer
            {
                HttpClientInitializer = credential,
                ApplicationName = "TotalCare Messaging"
            });
        }

        public async Task SendNotificationAsync(string deviceToken, string title, string body)
        {
            var message = new Message
            {
                Token = deviceToken,
                Notification =
                {
                    Title = title,
                    Body = body
                }
            };

            var request = new ProjectsResource.MessagesResource.SendRequest(
                _fcmService,
                new SendMessageRequest { Message = message },
                $"projects/{_projectId}"
            );

            var response = await request.ExecuteAsync();
            Console.WriteLine($"Message sent with ID: {response.Name}");
        }
    }

}
