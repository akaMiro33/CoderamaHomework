using CoderamaHomework.Data.Models;
using CoderamaHomework.ServiceContracts.CacheServiceContracts;
using CoderamaHomework.ServiceContracts.FormatterServiceContracts;
using CoderamaHomework.ServiceContracts.StorageServiceContracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;

namespace CoderamaHomework.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentController : ControllerBase
    {
        private readonly IDocumentStorage _storage;
        private readonly IEnumerable<IDocumentFormatter> _formatters;
        private readonly ICacheProvider _cache;

        public DocumentController(IDocumentStorage storage, IEnumerable<IDocumentFormatter> formatters, ICacheProvider cache)
        {
            _storage = storage;
            _formatters = formatters;
            _cache = cache;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Document document)
        {
            await _storage.StoreDocumentAsync(document);
            return CreatedAtAction(nameof(Get), new { id = document.Id }, document);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id, [FromHeader(Name = "Accept")] string accept)
        {
            var formatter = _formatters.FirstOrDefault(f => f.ContentType == accept);
            if (formatter == null)
            {
                return StatusCode(StatusCodes.Status406NotAcceptable);
            }

            string? content = null;

            if (_cache != null)
            {
                content = await _cache.GetAsync<string>(id + "_" + accept);
            }

            if (content == null)
            {
                var document = await _storage.GetDocumentAsync(id);
                if (document == null)
                {
                    return NotFound();
                }

                content = await formatter.SerializeAsync(document);

                if (_cache != null)
                {
                    await _cache.SetAsync<string>(document.Id + "_" + accept, content, new DistributedCacheEntryOptions
                    {
                        AbsoluteExpirationRelativeToNow = new TimeSpan(0, 0, 15, 0, 0),
                    });
                }
            }

            return Content(content, formatter.ContentType);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, [FromBody] Document document)
        {
            if (id != document.Id)
                return BadRequest();

            await _storage.UpdateDocumentAsync(document);
            return NoContent();
        }
    }
}
