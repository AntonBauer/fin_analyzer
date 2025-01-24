using System.Net;
using System.Net.Http.Json;
using FinAnalyzer.Api.Requests;
using FinAnalyzer.Domain.Models;
using FinAnalyzer.Domain.Rules;

namespace FinAnalyzer.Tests.Integration;

[TestFixture]
internal sealed class RulesTests : IntegrationTestBase
{
    private Category? _testCategory;
    private RegexRuleDto? _testRule;
    private Suggestion[]? _suggestions;

    [OneTimeSetUp]
    public async Task SetUp()
    {
        await UploadTransactions();
        _testCategory = await CreateCategory();
    }

    [OneTimeTearDown]
    public void TearDown()
    {
        _testCategory = null;
        _testRule = null;
        _suggestions = null;
    }

    [Test, Order(0)]
    public async Task Should_create_rule()
    {
        // Act
        var response = await Client.PostAsJsonAsync("/rules",
                                                    new CreateRegexRuleRequest
                                                    {
                                                        Name = "Test rule",
                                                        Expression = @"(?i)(?:\bkaufland\b)|(?:\bdm\b)|(?:\b35314369001\b)",
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
        var rules = await Client.GetFromJsonAsync<RegexRuleDto[]>("/rules");

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(rules.Length, Is.EqualTo(1));
            Assert.That(rules[0].Name, Is.EqualTo("Test rule"));
        });

        _testRule = rules[0];
    }

    [Test, Order(2)]
    public async Task Should_apply_test_rule()
    {
        // Act
        var response = await Client.PostAsJsonAsync($"/rules/{_testRule.Value.Id}/apply", _testRule.Value);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created));
            Assert.That(response.Headers.Location, Is.Not.Null);
        });
    }

    [Test, Order(3)]
    public async Task Should_read_suggestions()
    {
        // Act
        var suggestions = await Client.GetFromJsonAsync<Suggestion[]>("/suggestions");

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(suggestions.Length, Is.EqualTo(3));
            Assert.That(suggestions[0].SuggestedCategory.Id, Is.EqualTo(_testCategory.Id));
        });

        _suggestions = suggestions;
    }

    [Test, Order(4)]
    public async Task Should_discard_suggestion()
    {

    }

    [Test, Order(5)]
    public async Task Should_apply_suggestion()
    {
        // Act
        var response = await Client.PostAsJsonAsync($"/suggestions/{_suggestions[1].Id}/apply", _suggestions[1].Id);
        var raw = await response.Content.ReadAsStringAsync();

        // Assert
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
    }

    [Test, Order(6)]
    public async Task Should_have_less_suggestions_open()
    {
        // Act
        var suggestions = await Client.GetFromJsonAsync<Suggestion[]>("/suggestions");

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(suggestions.Length, Is.EqualTo(2));
            Assert.That(suggestions[0].SuggestedCategory.Id, Is.EqualTo(_testCategory.Id));
        });

    }

    [Test, Order(7)]
    public async Task Should_delete_rule()
    {
        // Act
        var response = await Client.DeleteAsync($"/rules/{_testRule.Value.Id}");
        var rules = await Client.GetFromJsonAsync<RegexRuleDto[]>("/rules");

        Assert.Multiple(() =>
        {
            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(rules.Length, Is.EqualTo(0));
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
        await Client.PostAsJsonAsync("/categories", new CreateCategoryRequest { Name = "Test category" });
        var categories = await Client.GetFromJsonAsync<Category[]>("/categories");

        return categories.First();
    }
}