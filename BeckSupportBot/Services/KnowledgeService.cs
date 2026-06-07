using BeckSupportBot.Interfaces;
using BeckSupportBot.Models;

namespace BeckSupportBot.Services;

public class KnowledgeService : IKnowledgeService
{
    private readonly string _basePath;
    private List<KnowledgeItem>? _cachedKnowledgeItems;

    public KnowledgeService(IWebHostEnvironment environment)
    {
        _basePath = Path.Combine(environment.ContentRootPath, "KnowledgeBase");
    }

    public async Task<List<KnowledgeMatch>> FindRelevantContextsAsync(string question)
    {
        var knowledgeItems = await LoadKnowledgeItemsAsync();

        var questionWords = question
            .ToLower()
            .Split(" ", StringSplitOptions.RemoveEmptyEntries);

        
        const int minimumScore = 1;
        

        var matches = knowledgeItems
            .Select(item => new KnowledgeMatch
            {
                Title = item.Title,
                Category = item.Category,
                Content = item.Content,
                SourceFile = item.SourceFile,
                Score = CalculateScore(item, questionWords)
            })
            .Where(match => match.Score >= minimumScore)
            .OrderByDescending(match => match.Score)
            .Take(6)
            .ToList();

        return matches;
    }

    private async Task<List<KnowledgeItem>> LoadKnowledgeItemsAsync()
    {
        if (_cachedKnowledgeItems != null)
        {
            return _cachedKnowledgeItems;
        }

        var items = new List<KnowledgeItem>();

        if (!Directory.Exists(_basePath))
        {
            return items;
        }

        var txtFiles = Directory.GetFiles(_basePath, "*.txt", SearchOption.AllDirectories);
        var mdFiles = Directory.GetFiles(_basePath, "*.md", SearchOption.AllDirectories);

        var files = txtFiles.Concat(mdFiles).ToArray();

        foreach (var file in files)
        {
            var text = await File.ReadAllTextAsync(file);
            var extension = Path.GetExtension(file).ToLower();

            if (extension == ".md")
            {
                items.AddRange(ParseMarkdownFileIntoChunks(text, file));
            }
            else
            {
                items.Add(ParseTextKnowledgeFile(text, file));
            }
        }

        _cachedKnowledgeItems = items;
        return _cachedKnowledgeItems;
    }

    private KnowledgeItem ParseTextKnowledgeFile(string text, string filePath)
    {
        var item = new KnowledgeItem
        {
            SourceFile = Path.GetFileName(filePath),
            Category = GetFolderName(filePath)
        };

        var lines = text.Split(new[] { "\r\n", "\n" }, StringSplitOptions.None);

        var contentLines = new List<string>();
        var isContent = false;

        foreach (var line in lines)
        {
            var trimmedLine = line.Trim();

            if (trimmedLine.StartsWith("Titel:", StringComparison.OrdinalIgnoreCase))
            {
                item.Title = trimmedLine.Replace("Titel:", "").Trim();
            }
            else if (trimmedLine.StartsWith("Kategori:", StringComparison.OrdinalIgnoreCase))
            {
                item.Category = trimmedLine.Replace("Kategori:", "").Trim();
            }
            else if (trimmedLine.StartsWith("Indhold:", StringComparison.OrdinalIgnoreCase))
            {
                isContent = true;
            }
            else if (isContent)
            {
                contentLines.Add(trimmedLine);
            }
        }

        item.Content = CleanMarkdown(string.Join(Environment.NewLine, contentLines)).Trim();

        if (string.IsNullOrWhiteSpace(item.Title))
        {
            item.Title = Path.GetFileNameWithoutExtension(filePath);
        }

        return item;
    }

    private List<KnowledgeItem> ParseMarkdownFileIntoChunks(string text, string filePath)
    {
        var chunks = new List<KnowledgeItem>();

        var lines = text.Split(new[] { "\r\n", "\n" }, StringSplitOptions.None);

        var fileTitle = Path.GetFileNameWithoutExtension(filePath);
        var category = GetFolderName(filePath);

        string? currentSectionTitle = null;
        var currentContent = new List<string>();

        foreach (var line in lines)
        {
            var trimmedLine = line.Trim();

            if (trimmedLine.StartsWith("# ") && fileTitle == Path.GetFileNameWithoutExtension(filePath))
            {
                fileTitle = CleanMarkdown(trimmedLine.Replace("#", "").Trim());
                continue;
            }

            if (trimmedLine.StartsWith("**Kategori:**", StringComparison.OrdinalIgnoreCase))
            {
                category = trimmedLine
                    .Replace("**Kategori:**", "", StringComparison.OrdinalIgnoreCase)
                    .Trim();

                continue;
            }

            if (trimmedLine.StartsWith("## ")
                || trimmedLine.StartsWith("### ")
                || trimmedLine.StartsWith("#### "))
            {
                AddChunkIfValid(
                    chunks,
                    fileTitle,
                    currentSectionTitle,
                    currentContent,
                    category,
                    filePath
                );

                currentSectionTitle = CleanMarkdown(trimmedLine);
                currentContent = new List<string>();

                continue;
            }

            if (ShouldIgnoreLine(trimmedLine))
            {
                continue;
            }

            if (currentSectionTitle != null)
            {
                currentContent.Add(trimmedLine);
            }
        }

        AddChunkIfValid(
            chunks,
            fileTitle,
            currentSectionTitle,
            currentContent,
            category,
            filePath
        );

        if (!chunks.Any())
        {
            chunks.Add(new KnowledgeItem
            {
                Title = fileTitle,
                Category = category,
                Content = CleanMarkdown(text),
                SourceFile = Path.GetFileName(filePath)
            });
        }

        return chunks;
    }

    private void AddChunkIfValid(
        List<KnowledgeItem> chunks,
        string fileTitle,
        string? sectionTitle,
        List<string> contentLines,
        string category,
        string filePath)
    {
        if (string.IsNullOrWhiteSpace(sectionTitle))
        {
            return;
        }

        if (ShouldIgnoreSection(sectionTitle))
        {
            return;
        }

        var content = CleanMarkdown(string.Join(Environment.NewLine, contentLines)).Trim();

        if (content.Length < 50)
        {
            return;
        }

        chunks.Add(new KnowledgeItem
        {
            Title = $"{fileTitle} - {sectionTitle}",
            Category = category,
            Content = content,
            SourceFile = Path.GetFileName(filePath)
        });
    }

    private int CalculateScore(KnowledgeItem item, string[] questionWords)
    {
        var title = item.Title.ToLower();
        var category = item.Category.ToLower();
        var content = item.Content.ToLower();

        var stopWords = new List<string>
    {
        "hvordan", "hvad", "hvor", "hvorfor", "hvilken", "hvilke",
        "jeg", "du", "en", "et", "den", "det", "de",
        "er", "og", "i", "på", "for", "til", "med", "af",
        "skal", "kan", "man", "mig", "min", "mit", "mine",
        "virker", "fungerer", "bruges", "laver", "gør"
    };

        var score = 0;

        foreach (var originalWord in questionWords)
        {
            var word = NormalizeWord(originalWord);

            if (word.Length < 3) continue;
            if (stopWords.Contains(word)) continue;

            var searchWords = ExpandSearchWords(word);
            


            foreach (var searchWord in searchWords)
            {
                if (title.Contains(searchWord))
                {
                    score += 6;
                }

                if (category.Contains(searchWord))
                {
                    score += 2;
                }

                if (content.Contains(searchWord))
                {
                    score += 1;
                }

                if (searchWord.Contains("lcd"))
                {
                    if (title.Contains(".lcd")) score += 10;
                    if (content.Contains(".lcd")) score += 5;
                }
            }
        }

        foreach (var qWord in questionWords)
        {
            var cleanWord = NormalizeWord(qWord);

            if (int.TryParse(cleanWord, out _))
            {
                if (title.Contains(cleanWord))
                {
                    score += 8;
                }

                if (content.Contains(cleanWord))
                {
                    score += 3;
                }
            }
        }

        if (item.Content.Length < 3000)
        {
            score += 3;
        }

        return score;
    }

    private List<string> ExpandSearchWords(string word)
    {
        var words = new List<string> { word };

        if (word.Contains("score") || word.Contains("scor") || word.Contains("point"))
        {
            words.AddRange(new[]
            {
            "resultat", "result", "indtast", "registrering",
            "bordresultat", "matchpoint", "procent"
        });
        }

        if (word.Contains("resultat") || word.Contains("result"))
        {
            words.AddRange(new[]
            {
            "score", "scor", "result", "bordresultat",
            "indtast", "registrering", "matchpoint"
        });
        }

        if (word.Contains("board") || word.Contains("spil"))
        {
            words.AddRange(new[]
            {
            "board", "boardspec", "spil", "kortfordeling", "kort"
        });
        }

        if (word.Contains("runde") || word.Contains("round"))
        {
            words.AddRange(new[]
            {
            "round", "roundmatch", "runde", "bordopstilling"
        });
        }

        if (word.Contains("sektion") || word.Contains("section"))
        {
            words.AddRange(new[]
            {
            "section", "sektion", "spilleaften", "række"
        });
        }

        if (word.Contains("spiller") || word.Contains("par") || word.Contains("deltager"))
        {
            words.AddRange(new[]
            {
            "sectionplayer", "sectionteam", "startliste",
            "deltager", "spiller", "par"
        });
        }

        if (word.Contains("skifte") || word.Contains("movement"))
        {
            words.AddRange(new[]
            {
            "movement", "movementplan", "skifteplan", "bordplacering"
        });
        }

        if (word.Contains("turnering"))
        {
            words.AddRange(new[]
            {
            "maintournament", "grouptournament", "klubturnering",
            "turneringsopsætning"
        });
        }

        if (word.Contains("funding"))
        {
            words.AddRange(new[]
            {
            "funding", "skifteplan", "par", "grupper", "opdeling"
        });
        }

        if (word.Contains("enkeltmand") || word.Contains("individuel"))
        {
            words.AddRange(new[]
            {
            "enkeltmand", "em turnering", "spillere",
            "skifteplan", "type 3"
        });
        }

        if (word.Contains("handicap") || word.Contains("hac"))
        {
            words.AddRange(new[]
            {
            "hac", "hacround", "hacroundplayer",
            "handicapberegning", "hacstart",
            "hacdelta", "expectedscorepct",
            "achievedscorepct"
        });
        }

        if (word.Contains("mitchell"))
        {
            words.AddRange(new[]
            {
            "mitchell", "hopmetode", "spring", "flyttepar"
        });
        }

        if (word.Contains("howell"))
        {
            words.AddRange(new[]
            {
            "howell", "uendelig howell", "alle møder alle"
        });
        }

        if (word.Contains("lcd"))
        {
            words.AddRange(new[]
            {
            "lcd", "skifteplan", "movementplan", "turneringsformat"
        });
        }

        return words.Distinct().ToList();
    }

    private string NormalizeWord(string word)
    {
        word = word
            .ToLower()
            .Replace("?", "")
            .Replace(".", "")
            .Replace(",", "")
            .Replace("!", "")
            .Replace(":", "")
            .Replace(";", "")
            .Trim();

        var endings = new[]
        {
            "erne",
            "ende",
            "else",
            "eren",
            "ingen",
            "ninger",
            "ning",
            "er",
            "en",
            "et",
            "e",
            "s"
        };

        foreach (var ending in endings)
        {
            if (word.EndsWith(ending) && word.Length > ending.Length + 3)
            {
                return word[..^ending.Length];
            }
        }

        return word;
    }

    private string CleanMarkdown(string text)
    {
        return text
            .Replace("### ", "")
            .Replace("## ", "")
            .Replace("# ", "")
            .Replace("**", "")
            .Replace("---", "")
            .Replace("- ", "")
            .Trim();
    }

    private bool ShouldIgnoreSection(string sectionTitle)
    {
        var title = sectionTitle.ToLower();

        return title.Contains("foreløbige nøgleord")
            || title.Contains("nøgleord")
            || title.Contains("mangler")
            || title.Contains("ai-instruktion")
            || title.Contains("upload-overblik")
            || title.Contains("anbefalet upload")
            || title.Contains("praktisk anbefaling")
            || title.Contains("stop og eskaler")
            || title.Contains("kort konklusion")
            || title.Contains("sikker at omtale")
            || title.Contains("brug som customgpt")
            || title.Contains("customgpt")
            || title.Contains("filsamling");
    }

    private bool ShouldIgnoreLine(string line)
    {
        return string.IsNullOrWhiteSpace(line)
            || line.StartsWith("*Kilde:", StringComparison.OrdinalIgnoreCase);
    }

    private string GetFolderName(string filePath)
    {
        return Directory.GetParent(filePath)?.Name ?? "General";
    }
}
