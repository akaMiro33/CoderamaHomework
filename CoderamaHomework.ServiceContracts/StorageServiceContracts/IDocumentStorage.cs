using CoderamaHomework.Data.Models;

namespace CoderamaHomework.ServiceContracts.StorageServiceContracts
{
    public interface IDocumentStorage
    {
        Task StoreDocumentAsync(Document document);
        Task<Document> GetDocumentAsync(string id);
        Task UpdateDocumentAsync(Document document);
    }
}
