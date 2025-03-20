using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using CBRE.FacilityManagement.Audit.Application.Contracts.Infrastructure;

namespace CBRE.FacilityManagement.Audit.Infrastructure
{
    public class AIService : IAIService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;

        public AIService(string endpoint, string apiKey)
        {
            _httpClient = new HttpClient { BaseAddress = new Uri(endpoint) };
            _apiKey = apiKey;
        }

        public async Task<string> GenerateSummaryAsync(List<string> filePaths)
        {
            try
            {
                // Read the content of each file
                List<string> documents = new List<string>();
                foreach (var filePath in filePaths)
                {
                    if (File.Exists(filePath))
                    {
                        string contents = await File.ReadAllTextAsync(filePath);
                        documents.Add(contents);
                    }
                    else
                    {
                        throw new FileNotFoundException($"The file {filePath} does not exist.");
                    }
                }

                // Concatenate all documents into one string with clear separators
                string concatenatedDocuments = string.Join("\n\n---\n\n", documents);

                // Create a prompt to ask the model to summarize the documents
                string prompt = $"Please summarize the following documents:\n\n{concatenatedDocuments}\n\nSummary:";

                // Create the request payload
                var requestBody = new
                {
                    model = "gpt-4o-mini-2",
                    prompt = prompt,
                    max_tokens = 150
                };

                var content = new StringContent(JsonSerializer.Serialize(requestBody), Encoding.UTF8, "application/json");

                // Add the API key to the request headers
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _apiKey);

                // Make the request to the OpenAI API
                var response = await _httpClient.PostAsync("/v1/completions?api-version=2024-05-01-preview", content);
                response.EnsureSuccessStatusCode();

                var responseContent = await response.Content.ReadAsStringAsync();
                var responseJson = JsonSerializer.Deserialize<JsonElement>(responseContent);

                // Return the summary text
                return responseJson.GetProperty("choices")[0].GetProperty("text").GetString().Trim();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}