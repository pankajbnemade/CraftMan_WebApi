using System.Text.Json;
using System.Text;
using System.Net.Http.Headers;


namespace CraftMan_WebApi.Models
{
    public class FCMNotification
    {
        private readonly string _serverKey = "YOUR_FCM_SERVER_KEY";
        private readonly string _senderId = "131618401442";

        public async Task<bool> SendNotification(string fcmToken, string title, string body)
        {
            var url = "https://fcm.googleapis.com/fcm/send";
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("key", "=" + _serverKey);
            httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Sender", "id=" + _senderId);

            var message = new
            {
                to = fcmToken,
                notification = new { title, body, sound = "default" },
                data = new { customData = "Ticket Update" }
            };

            var jsonMessage = JsonSerializer.Serialize(message);
            var content = new StringContent(jsonMessage, Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync(url, content);
            return response.IsSuccessStatusCode;
        }
    }

}
