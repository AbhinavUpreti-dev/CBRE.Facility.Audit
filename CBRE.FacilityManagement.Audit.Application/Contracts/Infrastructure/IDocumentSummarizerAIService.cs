
public interface IDocumentSummarizerAIService
{
    string GenerateSummaryAsync(List<string> documents, string extension);

    Task<string> GenerateAuditSummaryAsync(string jsonText);
}