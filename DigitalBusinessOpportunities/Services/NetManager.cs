
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Policy;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Net.Http.Headers;


namespace DigitalBusinessOpportunities.Services
{
    public class NetManager
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;
        private readonly string _basicKey = "MDE5YWI1ZWEtMDhmNy03OWQyLWEzNzktYzVmZGY2OTA4Mjc2OmE5ZGVhZjcyLTU0ZTUtNDAwOC05ZTlhLTMzZDk0YzhiMWVjMg==";
        private readonly string _authUrl = "https://ngw.devices.sberbank.ru:9443/api/v2/oauth";
        private readonly string _clientId = "019ab5ea-08f7-79d2-a379-c5fdf6908276";

        private string _accesToken = "";
        private DateTime _expiresAt;

        public NetManager(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<TResponce> PostAsync<TRequest, TResponce>(string url, TRequest body)
        {
            await EnsureAuthorizedAsync();
            _httpClient.DefaultRequestHeaders.Add("X-Client-ID", _clientId);
            var json = System.Text.Json.JsonSerializer.Serialize(body);
            var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

            var responce = await _httpClient.PostAsync(url, content);
            responce.EnsureSuccessStatusCode();

            var resultJson = await responce.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<TResponce>(resultJson);
        }

        public async Task AuthorizeAsync()
        {
            var request = new HttpRequestMessage(HttpMethod.Post, _authUrl);

            request.Headers.Add("Accept", "application/json");
            request.Headers.Add("RqUID", Guid.NewGuid().ToString());
            request.Headers.Add("Authorization", $"Basic {_basicKey}");

            request.Content = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                {"scope", "GIGACHAT_API_PERS"}
            });

            var responce = await _httpClient.SendAsync(request);
            responce.EnsureSuccessStatusCode();

            var json = await responce.Content.ReadAsStringAsync();

            var data = JsonSerializer.Deserialize<AuthResponse>(json);
            _accesToken = data.access_token;
            _expiresAt = DateTime.UtcNow.AddSeconds(data.ExpiresIn - 30);

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _accesToken);
        }

        public async Task EnsureAuthorizedAsync()
        {
            if (string.IsNullOrEmpty(_accesToken) || DateTime.UtcNow >= _expiresAt)
            {
                await AuthorizeAsync();
            }
            if (!_httpClient.DefaultRequestHeaders.Contains("X-Client-ID"))
            {
                _httpClient.DefaultRequestHeaders.Add("X-Client-ID", _clientId);
            }
        }

        public class AuthResponse
        {
            public string access_token { get; set; }
            public int ExpiresIn { get; set; }
            public string TokenType { get; set; }
        }
    }
}
