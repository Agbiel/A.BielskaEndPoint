using A.Bielska_EndPoint.Models;
using A.Bielska_EndPoint.Services;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddOpenApi();

builder.Services.AddSingleton<ContentParserService>();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.MapPost("/api/v1/parse-content", (ParseContentRequest request, ContentParserService parser) =>
{
    if (string.IsNullOrWhiteSpace(request.Content))
    {
        return Results.BadRequest("Pole 'content' nie może być puste.");
    }

    string decodedContent;

    try
    {
        byte[] bytes = Convert.FromBase64String(request.Content);
        decodedContent = System.Text.Encoding.UTF8.GetString(bytes);
    }
    catch
    {
        return Results.BadRequest("Niepoprawny format Base64.");
    }

    object parsedData;
    int itemsCount;

    try
    {
        switch (request.Type)
        {
            case ContentType.CSV: 
                var csvRecords = parser.ParseCsv(decodedContent);
                parsedData = csvRecords;
                itemsCount = csvRecords.Count;
                break;

            case ContentType.INTERNAL_JSON:
                var (jsonData, jsonCount) = parser.ParseInternalJson(decodedContent);
                parsedData = jsonData;
                itemsCount = jsonCount;
                break;

            default:
                return Results.BadRequest($"Nieobsługiwany typ zawartości: '{request.Type}'. Expected 'CSV' lub 'INTERNAL_JSON'.");
        }
    }
    catch (Exception ex)
    {
        return Results.BadRequest($"Błąd podczas parsowania zawartości: {ex.Message}");
    }


    return Results.Ok(new ParseContentResponse
    {
        Success = true,
        Count = itemsCount,
        Data = parsedData
    });
});

app.Run();