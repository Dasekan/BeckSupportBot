using BeckSupportBot.Models;

namespace BeckSupportBot.Models;

public class ChatResponse
{
    public string Answer { get; set; } = string.Empty;
    public List<KnowledgeMatch>? ContextUsed { get; set; }
}
