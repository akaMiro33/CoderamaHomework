using CoderamaHomework.Data.Models;
using CoderamaHomework.ServiceContracts.StorageServiceContracts;
using System.Collections.Concurrent;

namespace CoderamaHomework.Services.StorageServices;

public class InMemoryDocumentStorage : IDocumentStorage
{
    private readonly ConcurrentDictionary<string, Document> _storage = new ConcurrentDictionary<string, Document>();

    public Task StoreDocumentAsync(Document document)
    {
        _storage[document.Id] = document;
        return Task.CompletedTask;
    }
    public Task<Document> GetDocumentAsync(string id)
    {
        _storage.TryGetValue(id, out var document);
        return Task.FromResult(document);
    }

    public Task UpdateDocumentAsync(Document document)
    {
        _storage[document.Id] = document;
        return Task.CompletedTask;
    }
}
