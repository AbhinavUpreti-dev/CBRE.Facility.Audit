
public interface IDocumentSummarizerAIService
{
    string ExtractTextFromPdf(string filePath);
    string GenerateSummaryAsync(List<string> documents, string extension);
}