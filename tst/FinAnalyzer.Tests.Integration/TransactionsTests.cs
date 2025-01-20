using System.Net;

namespace FinAnalyzer.Tests.Integration;

[TestFixture]
internal sealed class AccountTests : IntegrationTestBase
{
    [Test, Order(0)]
    public async Task Should_upload_ing_data()
    {
        // Arrange
        var content = await CreateContent("files/test_1.csv", "transactionsFile");

        // Act
        var response = await Client.PostAsync("/upload-ing-data", content);
        var raw = await response.Content.ReadAsStringAsync();

        // Assert
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created));
    }

    [Test, Order(1)]
    public async Task Should_read_account_info()
    {
        // Arrange

        // Act

        // Assert
        Assert.Fail();
    }

    private static async Task<MultipartFormDataContent> CreateContent(string filePath, string name)
    {
        var data = await File.ReadAllBytesAsync(filePath);

        var content = new MultipartFormDataContent();
        content.Add(new ByteArrayContent(data), name, name);

        return content;
    }
}
