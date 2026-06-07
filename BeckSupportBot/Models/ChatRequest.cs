using System.ComponentModel.DataAnnotations;

namespace BeckSupportBot.Models;

public class ChatRequest
{
    [Required(ErrorMessage = "Spørgsmålet må ikke være tomt.")]
    [MinLength(3, ErrorMessage = "Spørgsmålet skal være mindst 3 tegn langt.")]
    [MaxLength(500, ErrorMessage = "Spørgsmålet må højst være 500 tegn langt.")]
    public string Question { get; set; } = string.Empty;
}
