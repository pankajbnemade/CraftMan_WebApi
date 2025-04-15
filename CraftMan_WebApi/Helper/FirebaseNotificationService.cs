namespace CraftMan_WebApi.Helper
{
    using Google.Apis.Auth.OAuth2;
    using System.Net.Http.Headers;
    using System.Text;
    using System.Text.Json;

    public class FirebaseNotificationService
    {
        private readonly IConfiguration _config;

        public FirebaseNotificationService(IConfiguration config)
        {
            _config = config;
        }

        public async Task SendNotificationAsync(List<string> tokens, string title, string body)
        {
            using HttpClient client = new();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            foreach (var token in tokens)
            {
                var payload = new
                {
                    to = token,
                    title = title,
                    body = body,
                    sound = "default"
                };

                var jsonPayload = JsonSerializer.Serialize(payload);
                var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

                var response = await client.PostAsync("https://exp.host/--/api/v2/push/send", content);
                var result = await response.Content.ReadAsStringAsync();

                Console.WriteLine($"📲 Expo Push sent to {token}: {response.StatusCode} - {result}");
            }
        }


    }


}
