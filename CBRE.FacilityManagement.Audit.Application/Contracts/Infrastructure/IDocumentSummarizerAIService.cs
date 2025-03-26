
public interface IDocumentSummarizerAIService
{
    string GenerateSummaryAsync(List<string> documents, string extension);

    Task<string> GenerateAuditSummaryAsync(string jsonText, string prompt, string assetJson);
    Task<string> PerformComplianceCheckAsync(string documentText);
}