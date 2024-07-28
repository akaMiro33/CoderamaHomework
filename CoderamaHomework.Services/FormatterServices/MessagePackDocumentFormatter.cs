using MessagePack;
using CoderamaHomework.Data.Models;
using CoderamaHomework.ServiceContracts.FormatterServiceContracts;

namespace CoderamaHomework.Services.FormatterServices;

public class MessagePackDocumentFormatter : IDocumentFormatter
{
    public string ContentType => "application/x-msgpack";

    public async Task<string> SerializeAsync(Document document)
    {
        var jsonResult = System.Text.Json.JsonSerializer.Serialize(document);
        var result = MessagePackSerializer.ConvertFromJson(jsonResult);
        return Convert.ToBase64String(result.ToArray());
    }
}
