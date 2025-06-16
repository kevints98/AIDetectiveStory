using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using DetectiveAI.Models;
using System.Text.Json;
using Microsoft.Extensions.Configuration;

namespace DetectiveAI.AI
{
    internal class GroqClient : ILLMClient
    {
        private readonly HttpClient _httpClient;
        private readonly string _endpoint;
        private readonly string _model;

        public GroqClient()
        {
            var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            var apiKey = config["Groq:ApiKey"] ?? throw new Exception("Groq API key missing.");
            _endpoint = config["Groq:Endpoint"]!;
            _model = config["Groq:Model"]!;

            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);
        }

        public async Task<string> GetResponseAsync(List<Message> messages)
        {
            var requestBody = new
            {
                model = _model,
                messages = messages.Select(m => new { role = m.Role, content = m.Content }),
                temperature = 0.7
            };

            var response = await _httpClient.PostAsync(
                _endpoint,
                new StringContent(JsonSerializer.Serialize(requestBody), Encoding.UTF8, "application/json")
            );

            var responseString = await response.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var parsed = JsonSerializer.Deserialize<LLMResponse>(responseString, options);

            return parsed?.Choices?.FirstOrDefault()?.Message?.Content ?? "Geen antwoord ontvangen.";

        }
    }
}
