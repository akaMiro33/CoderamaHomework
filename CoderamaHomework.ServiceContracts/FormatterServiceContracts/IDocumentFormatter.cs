using CoderamaHomework.Data.Models;

namespace CoderamaHomework.ServiceContracts.FormatterServiceContracts
{
    public interface IDocumentFormatter
    {
        Task<string> SerializeAsync(Document document);
        string ContentType { get; }
    }
}
