using System.Net;
using System.Net.Http.Json;
using FinAnalyzer.Api.Dtos;
using FinAnalyzer.Domain.Models;
using FinAnalyzer.Domain.Rules;

namespace FinAnalyzer.Tests.Integration;

[TestFixture]
internal sealed class RulesTests : IntegrationTestBase
{
    private Category? _testCategory;

    [Test, Order(0)]
    public async Task Should_create_rule()
    {
        // Arrange
        await UploadTransactions();
        _testCategory = await CreateCategory();

        // Act
        var response = await Client.PostAsJsonAsync("/rules",
                                                    new CreateRegexRuleDto
                                                    {
                                                        Name = "Test rule",
                                                        Expression = @"(?i)\bKaufland\b",
                                                        PropertyToCheck = TransactionProperty.Purpose,
                                                        SuggestedCategoryId = _testCategory.Id
                                                    });

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created));
            Assert.That(response.Headers.Location, Is.Not.Null);
        });
    }

    [Test, Order(1)]
    public async Task Should_read_all_rules()
    {
        // Act
        var rules = await Client.GetFromJsonAsync<RegexRule[]>("/rules");

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(rules.Length, Is.EqualTo(1));
            Assert.That(rules[0].Name, Is.EqualTo("Test rule"));
        });
    }
    
    private async Task UploadTransactions()
    {
        var data = await File.ReadAllBytesAsync("files/test_1.csv");
        var content = new MultipartFormDataContent { { new ByteArrayContent(data), "transactionsFile", "transacdtionsFile" } };
        await Client.PostAsync("/upload-ing-data", content);
    }

    private async Task<Category> CreateCategory()
    {
        await Client.PostAsJsonAsync("/categories", new CreateCategoryDto { Name = "Test category" });
        var categories = await Client.GetFromJsonAsync<Category[]>("/categories");

        return categories.First();
    }
}