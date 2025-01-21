using System.Net;
using System.Net.Http.Json;
using FinAnalyzer.Api.Dtos;
using FinAnalyzer.Domain.Models;

namespace FinAnalyzer.Tests.Integration;

[TestFixture]
internal sealed class CategoryTests : IntegrationTestBase
{
    private string? _categoryLocation;

    [Test, Order(0)]
    public async Task Should_create_category()
    {
        // Act
        var response = await Client.PostAsJsonAsync("/categories", new CreateCategoryDto { Name = "Test category" });

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created));
            Assert.That(response.Headers.Location, Is.Not.Null);
        });

        _categoryLocation = response.Headers.Location.ToString();
    }

    [Test, Order(1)]
    public async Task Should_read_all_categories()
    {
        // Act
        var categories = await Client.GetFromJsonAsync<Category[]>("/categories");

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(categories, Is.Not.Null);
            Assert.That(categories, Has.Length.EqualTo(1));
        });
    }

    [Test, Order(2)]
    public async Task Should_delete_category()
    {
        // Act
        await Client.DeleteAsync(_categoryLocation);
        var categories = await Client.GetFromJsonAsync<Category[]>("/categories");

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(categories, Is.Not.Null);
            Assert.That(categories, Is.Empty);
        });
    }
}