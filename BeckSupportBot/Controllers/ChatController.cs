using BeckSupportBot.Interfaces;
using BeckSupportBot.Models;
using Microsoft.AspNetCore.Mvc;

namespace BeckSupportBot.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ChatController : ControllerBase
{
    private readonly IAiService _aiService;
    private readonly IKnowledgeService _knowledgeService;

    public ChatController(IAiService aiService, IKnowledgeService knowledgeService)
    {
        _aiService = aiService;
        _knowledgeService = knowledgeService;
    }

    [HttpPost]
    public async Task<ActionResult<ChatResponse>> Ask(ChatRequest request)
    {
        if (!ModelState.IsValid)
        {
            var errorMessage = ModelState.Values
                .SelectMany(v => v.Errors)
                .FirstOrDefault()?.ErrorMessage ?? "Ugyldigt spørgsmål.";

            return BadRequest(new ErrorResponse
            {
                Message = errorMessage
            });
        }

        try
        {
            var matches = await _knowledgeService.FindRelevantContextsAsync(request.Question);

            var context = matches.Any()
                ? string.Join("\n\n---\n\n", matches.Select(m =>
                    $"Titel: {m.Title}\nKategori: {m.Category}\nScore: {m.Score}\nIndhold:\n{LimitText(m.Content, 2500)}"))
                : null;

            var answer = await _aiService.AskAsync(request.Question, context);

            return Ok(new ChatResponse
            {
                Answer = answer,
                ContextUsed = matches
            });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new ErrorResponse
            {
                Message = ex.Message
            });
        }
    }

    private static string LimitText(string text, int maxLength)
    {
        if (string.IsNullOrWhiteSpace(text))
        {
            return string.Empty;
        }

        if (text.Length <= maxLength)
        {
            return text;
        }

        return text.Substring(0, maxLength) + "...";
    }
}
