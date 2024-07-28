using CoderamaHomework.Data.Models;
using CoderamaHomework.ServiceContracts.FormatterServiceContracts;
using System.Text.Json;

namespace CoderamaHomework.Services.FormatterServices;

public class JsonDocumentFormatter : IDocumentFormatter
{
    public string ContentType => "application/json";

    public Task<string> SerializeAsync(Document document)
    {
        return Task.FromResult(JsonSerializer.Serialize(document));
    }
}
