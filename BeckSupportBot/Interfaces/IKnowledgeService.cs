using BeckSupportBot.Models;

namespace BeckSupportBot.Interfaces;

public interface IKnowledgeService
{
    Task<List<KnowledgeMatch>> FindRelevantContextsAsync(string question);
}
