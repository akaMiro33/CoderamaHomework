using CoderamaHomework.Data.Models;
using CoderamaHomework.ServiceContracts.FormatterServiceContracts;
using Newtonsoft.Json;
using System.Xml.Linq;

namespace CoderamaHomework.Services.FormatterServices;

public class XmlDocumentFormatter : IDocumentFormatter
{
    public string ContentType => "application/xml";

    public Task<string> SerializeAsync(Document document)
    {
        var jsonResult = System.Text.Json.JsonSerializer.Serialize(document);
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
        XNode node = JsonConvert.DeserializeXNode(jsonResult, "Root");
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
        return Task.FromResult(node.ToString());
    }
}
