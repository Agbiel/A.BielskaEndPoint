using System.Text.Json;
using A.Bielska_EndPoint.Models;

namespace A.Bielska_EndPoint.Services;

public class ContentParserService
{
    public List<ParsedRecord> ParseCsv(string text)
    {
        var result = new List<ParsedRecord>();

        var lines = text.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries);

        foreach (var line in lines)
        {
            var record = new ParsedRecord
            {
                Values = line.Split(',').ToList()
            };

            result.Add(record);
        }

        return result;
    }
    public (object ParsedData, int Count) ParseInternalJson(string jsonText)
    {
        using var document = JsonDocument.Parse(jsonText);
        var root = document.RootElement.Clone();

        
        int count = root.ValueKind == JsonValueKind.Array 
            ? root.GetArrayLength() 
            : 1;

        return (root, count);
    }
}
