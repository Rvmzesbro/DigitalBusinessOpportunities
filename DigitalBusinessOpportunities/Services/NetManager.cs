using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace DigitalBusinessOpportunities.Services
{
    public static class NetManager
    {
        public static string url = "https://gigachat.devices.sberbank.ru/";
      

        private const string AuthUrl = "https://ngw.devices.sberbank.ru:9443/api/v2/oauth";

        // Используем статический HttpClient, но с Handler для обхода проблем с SSL (специфика Сбера)
        private static readonly HttpClient httpClient;

        static NetManager()
        {
            // Настройка для игнорирования ошибок SSL (частая проблема с сертификатами Сбера)
            var handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true;

            httpClient = new HttpClient(handler);
        }

        public static async Task<string> GetTokenAsync(string authKey)
        {
            // 1. Создаем уникальный ID запроса (или используйте статический, как в примере Python)
            string rquid = Guid.NewGuid().ToString();

            // 2. Создаем запрос
            using (var request = new HttpRequestMessage(HttpMethod.Post, AuthUrl))
            {
                // 3. Добавляем заголовки
                // Authorization: Basic <Key>
                request.Headers.Authorization = new AuthenticationHeaderValue("Basic", authKey);
                // RqUID
                request.Headers.Add("RqUID", rquid);
                // Accept: application/json
                request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // 4. Формируем тело запроса (x-www-form-urlencoded)
                var collection = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("scope", "GIGACHAT_API_PERS")
            };

                request.Content = new FormUrlEncodedContent(collection);

                // 5. Отправляем
                try
                {
                    var response = await httpClient.SendAsync(request);

                    // Считываем ответ
                    string responseContent = await response.Content.ReadAsStringAsync();

                    if (!response.IsSuccessStatusCode)
                    {
                        Console.WriteLine($"Ошибка: {response.StatusCode}");
                        return null;
                    }

                    return responseContent; // Вернет JSON с токеном
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Исключение: {ex.Message}");
                    return null;
                }
            }
        }
    }
}
