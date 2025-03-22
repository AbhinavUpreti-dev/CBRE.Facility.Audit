using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CBRE.FacilityManagement.Audit.Application.Contracts.Infrastructure;
using CBRE.FacilityManagement.Audit.Infrastructure;
namespace ConsoleApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            string endpoint = " https://innovate2025te5669273486.openai.azure.com/";
            string apiKey = "4vdTuPSyyJV0ZA61FqXpH6EArgsuB0kG28KpW8e3fp3EYdb0FqZzJQQJ99BCACHYHv6XJ3w3AAAAACOG04IK";

            //var aiService = new AIService(endpoint, apiKey);

            //List<string> filePaths = new List<string>
            //{
            //    "C:\\Users\\AUpreti\\Downloads\\Holiday2024.pdf",
            //};

            //try
            //{
            //    string summary = await aiService.GenerateSummaryAsync(filePaths);
            //    Console.WriteLine("Summary:");
            //    Console.WriteLine(summary);
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine($"An error occurred: {ex.Message}");
            //}
            //string endpoint = Environment.GetEnvironmentVariable("AZURE_OPENAI_ENDPOINT") ?? "https://innovate1234.openai.azure.com/";

            //string apiKey = Environment.GetEnvironmentVariable("AZURE_OPENAI_API_KEY") ?? "<your-api-key>";

            string deploymentName = "gpt-4o";

            var summarizer = new DocumentSummarizerAIService(endpoint, apiKey, deploymentName);

            // Example of local file paths (replace with actual file paths)

            var documentPaths = new List<string>

                    {

                        @"C:\\Users\\AUpreti\\Downloads\\Holiday2024.pdf",
                    };

            // Read documents from local paths

            List<string> documents = summarizer.ReadDocumentsFromLocalPaths(documentPaths);

            // Generate the summary

            string summary = summarizer.GenerateSummaryAsync(documents,"pdf");

            // Print the generated summary

            Console.WriteLine("Summary:\n");

            Console.WriteLine(summary);

        }
    }
}
