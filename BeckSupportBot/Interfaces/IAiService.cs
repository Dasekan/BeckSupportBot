namespace BeckSupportBot.Interfaces;

public interface IAiService
{
    Task<string> AskAsync(string question, string? context = null);
}
