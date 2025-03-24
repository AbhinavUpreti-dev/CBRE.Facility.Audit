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

namespace CBRE.FacilityManagement.Audit.Infrastructure
{
    public class DocumentSummarizerAIService : IDocumentSummarizerAIService
    {
        private readonly AzureOpenAIClient _openAIClient;
        private readonly string _deploymentName;

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

            // Summarize each document individually if they are too long
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
                string prompt = $"Please summarize the following document:\n\n{documentText}\n\nSummary:";
                var messages = new List<ChatMessage>
            {
                new SystemChatMessage("You are a helpful assistant."),
                new UserChatMessage(prompt) // Ensure the prompt is within token limit
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

            // Combine individual summaries into one final summary
            string finalSummary = string.Join("\n\n---\n\n", individualSummaries);
            return finalSummary;
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



        public string ExtractTextFromPdf(string filePath)
        {
            try
            {

                StringBuilder text = new StringBuilder();
                using (PdfReader reader = new PdfReader(filePath))
                using (PdfDocument pdfDoc = new PdfDocument(reader))
                {
                    for (int i = 1; i <= pdfDoc.GetNumberOfPages(); i++)
                    {
                        text.Append(PdfTextExtractor.GetTextFromPage(pdfDoc.GetPage(i)));
                    }
                }
                return text.ToString();

            }
            catch (Exception  ex)
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
    }
}
