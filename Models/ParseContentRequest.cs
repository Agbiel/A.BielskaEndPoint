namespace A.Bielska_EndPoint.Models;

public class ParseContentRequest
{
    public ContentType Type { get; set; }

    public string Content { get; set; } = string.Empty;
}