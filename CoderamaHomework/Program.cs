using CoderamaHomework;
using CoderamaHomework.ServiceContracts.CacheServiceContracts;
using CoderamaHomework.ServiceContracts.FormatterServiceContracts;
using CoderamaHomework.ServiceContracts.StorageServiceContracts;
using CoderamaHomework.Services.CacheServices;
using CoderamaHomework.Services.FormatterServices;
using CoderamaHomework.Services.StorageServices;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddMemoryCache();
builder.Services.AddSingleton<ICacheProvider, CacheMemoryProvider>();
builder.Services.AddSingleton<IDocumentStorage, InMemoryDocumentStorage>();
builder.Services.AddSingleton<IDocumentFormatter, JsonDocumentFormatter>();
builder.Services.AddSingleton<IDocumentFormatter, XmlDocumentFormatter>();
builder.Services.AddSingleton<IDocumentFormatter, MessagePackDocumentFormatter>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
