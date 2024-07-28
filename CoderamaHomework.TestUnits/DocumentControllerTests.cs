using CoderamaHomework.Controllers;
using CoderamaHomework.Data.Models;
using CoderamaHomework.Services.FormatterServices;
using CoderamaHomework.Services.StorageServices;


//using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace CoderamaHomework.UnitTests
{
    [TestClass]
    public class DocumentControllerTests
    {
        [TestMethod]
        public void Get_IsJSON_ReturnEqualObject()
        {
            // Arrange
            var jsonFormatter = new JsonDocumentFormatter();
            var storage = new InMemoryDocumentStorage();

            var document = new Document()
            {
                Id = "1",
                Tags = new List<string>() { "C#", "CODERAMA", "Task" },
                Data = new object[]
                {
                    "1", "2", "3", "4", "5", "6",
                }
            };

            var expectedValue = JsonSerializer.Serialize(document);

            // Act
            var controller = new DocumentController(storage, [jsonFormatter], null);
            controller.Post(document).GetAwaiter();
            var result = (controller.Get("1", "application/json").Result as Microsoft.AspNetCore.Mvc.ContentResult);

            //Assert
            Assert.AreEqual(expectedValue, result.Content);
            Assert.AreEqual("application/json", result.ContentType);

        }
    }
}
