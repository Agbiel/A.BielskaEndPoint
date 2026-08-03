namespace A.Bielska_EndPoint.Models;

public class ParseContentResponse
{
    public bool Success { get; set; }

    public int Count { get; set; }

    public object? Data { get; set; }
}