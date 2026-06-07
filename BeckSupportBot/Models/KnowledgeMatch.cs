namespace BeckSupportBot.Models;

public class KnowledgeMatch
{
    public string Title { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public string SourceFile { get; set; } = string.Empty;
    public int Score { get; set; }
}
