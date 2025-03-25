using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Azure;
using Azure.AI.OpenAI;
using Newtonsoft.Json;
using OpenAI;
using OpenAI.Chat;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Parser;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using iText.Layout.Element;
using DocumentFormat.OpenXml.Packaging;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using Microsoft.Identity.Client;
using Azure.Identity;

namespace CBRE.FacilityManagement.Audit.Infrastructure
{
    public class DocumentSummarizerAIService : IDocumentSummarizerAIService
    {
        private readonly AzureOpenAIClient _openAIClient;
        private readonly string _deploymentName;
        const int maxTokenLimit = 128000;
        public DocumentSummarizerAIService(string endpoint, string apiKey, string deploymentName)
        {
            _deploymentName = deploymentName;
            var credentials = new AzureKeyCredential(apiKey);
            _openAIClient = new AzureOpenAIClient(new Uri(endpoint), credentials);
        }

        public string GenerateSummaryAsync(List<string> documents, string extension)
        {
            if (documents == null || documents.Count == 0)
            {
                return "⚠️ No documents provided.";
            }

            List<string> individualSummaries = new List<string>();
            foreach (var document in documents)
            {
                var documentText = document;
                if (extension.Contains("pdf"))
                {
                    documentText = ExtractTextFromPdf(documentText);
                }
                else if (extension.Contains("doc") || extension.Contains("docx"))
                {
                    documentText = ExtractTextFromDoc(documentText);
                }
                else if (extension.Contains("xls") || extension.Contains("xlsx"))
                {
                    documentText = ExtractTextFromExcel(documentText);
                }

                // Split document text into smaller chunks if necessary
                var chunks = SplitTextIntoChunks(documentText, maxTokenLimit / 2); // Adjust chunk size as needed
                foreach (var chunk in chunks)
                {
                    string prompt = $"You are an AI assistant. Please provide a concise and informative recommendations from the following document. Highlight the recommendations section if present:\n\n{chunk}\n\nSummary:";
                    var messages = new List<ChatMessage>
                    {
                        new SystemChatMessage("You are a helpful assistant."),
                        new UserChatMessage(prompt)
                    };

                    var chatClient = _openAIClient.GetChatClient(_deploymentName);
                    ChatCompletion completion;
                    try
                    {
                        completion = chatClient.CompleteChat(messages);
                    }
                    catch (Exception ex)
                    {
                        return $"⚠️ Error generating summary: {ex.Message}";
                    }

                    individualSummaries.Add(completion.Content[0].Text.ToString());
                }
            }

            string finalSummary = string.Join("\n\n---\n\n", individualSummaries);
            return finalSummary;
        }


        public async Task<string> GenerateAuditSummaryAsync(string jsonText)
        {
            if (string.IsNullOrEmpty(jsonText))
            {
                return "⚠️ No data provided.";
            }

            // Summarize each document individually if they are too long
            var chatClient = _openAIClient.GetChatClient(_deploymentName);

            var messages = new List<ChatMessage>
            {
                new SystemChatMessage("You are a helpful assistant."),
                new UserChatMessage($"Summarize the following JSON data: {jsonText}.Please provide a detailed summary including the key points of the json without including any words such as json or datasets in response and include title as Audit Summary.")
            };

            ChatCompletion completion;
            try
            {
                completion = await chatClient.CompleteChatAsync(messages);
            }
            catch (Exception ex)
            {
                return $"⚠️ Error generating summary: {ex.Message}";
            }

            return completion.Content[0].Text.ToString();
        }

        private List<string> SplitTextIntoChunks(string text, int chunkSize)
        {
            var chunks = new List<string>();
            for (int i = 0; i < text.Length; i += chunkSize)
            {
                chunks.Add(text.Substring(i, Math.Min(chunkSize, text.Length - i)));
            }
            return chunks;
        }


        //This method is to test open AI from Console application "TestOpenAI"
        public List<string> ReadDocumentsFromLocalPaths(List<string> filePaths)
        {
            List<string> documents = new List<string>();
            foreach (var filePath in filePaths)
            {
                try
                {
                    if (File.Exists(filePath))
                    {
                        string documentContent;
                        if (Path.GetExtension(filePath).Equals(".pdf", StringComparison.OrdinalIgnoreCase))
                        {
                            documentContent = ExtractTextFromPdf(filePath);
                        }
                        else
                        {
                            documentContent = File.ReadAllText(filePath);
                        }
                        documents.Add(documentContent);
                    }
                    else
                    {
                        Console.WriteLine($"⚠️ File not found: {filePath}");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"⚠️ Error reading file {filePath}: {ex.Message}");
                }
            }
            return documents;
        }



        //public string ExtractTextFromPdf(string filePath)
        //{
        //    try
        //    {

        //        StringBuilder text = new StringBuilder();
        //        using (PdfReader reader = new PdfReader(filePath))
        //        using (PdfDocument pdfDoc = new PdfDocument(reader))
        //        {
        //            for (int i = 1; i <= pdfDoc.GetNumberOfPages(); i++)
        //            {
        //                text.Append(PdfTextExtractor.GetTextFromPage(pdfDoc.GetPage(i)));
        //            }
        //        }
        //        return text.ToString();

        //    }
        //    catch (Exception  ex)
        //    {
        //        throw;
        //    }
        //}

        public string ExtractTextFromPdf(string filePath)
        {
            try
            {
                StringBuilder text = new StringBuilder();
                using (PdfReader reader = new PdfReader(filePath))
                using (PdfDocument pdfDoc = new PdfDocument(reader))
                {
                    int numberOfPages = Math.Min(2, pdfDoc.GetNumberOfPages());
                    for (int i = 1; i <= numberOfPages; i++)
                    {
                        text.Append(PdfTextExtractor.GetTextFromPage(pdfDoc.GetPage(i)));
                    }
                }
                return text.ToString();
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        public string ExtractTextFromDoc(string filePath)
        {
            try
            {
                StringBuilder text = new StringBuilder();
                using (WordprocessingDocument wordDoc = WordprocessingDocument.Open(filePath, false))
                {
                    foreach (var element in wordDoc.MainDocumentPart.Document.Body.Elements())
                    {
                        text.Append(element.InnerText);
                    }
                }
                return text.ToString();
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string ExtractTextFromExcel(string filePath)
        {
            StringBuilder text = new StringBuilder();
            using (FileStream file = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                IWorkbook workbook = new XSSFWorkbook(file);
                for (int i = 0; i < workbook.NumberOfSheets; i++)
                {
                    ISheet sheet = workbook.GetSheetAt(i);
                    for (int j = 0; j <= sheet.LastRowNum; j++)
                    {
                        IRow row = sheet.GetRow(j);
                        if (row != null)
                        {
                            foreach (ICell cell in row.Cells)
                            {
                                text.Append(cell.ToString() + " ");
                            }
                            text.AppendLine();
                        }
                    }
                }
            }
            return text.ToString();
        }

        public async Task<string> PerformComplianceCheckAsync(string documentText)
        {
            string prompt = $"You are an AI assistant. Please check the following document for compliance with the specified criteria:\n\n{documentText}\n\nCompliance Check Result:";
            var messages = new List<ChatMessage>
        {
            new SystemChatMessage("You are a helpful assistant."),
            new UserChatMessage(prompt)
        };

            var chatClient = _openAIClient.GetChatClient(_deploymentName);
            ChatCompletion completion;
            try
            {
                completion = await chatClient.CompleteChatAsync(messages);
            }
            catch (Exception ex)
            {
                return $"⚠️ Error performing compliance check: {ex.Message}";
            }

            return completion.Content[0].Text.ToString();
        }
    }
}
