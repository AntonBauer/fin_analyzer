﻿using System.Net;
using System.Net.Http.Json;
using FinAnalyzer.Api.Dtos;
using FinAnalyzer.Domain.Models;

namespace FinAnalyzer.Tests.Integration;

[TestFixture]
internal sealed class AccountTests : IntegrationTestBase
{
    private string? _accountLocation;
    private string? _categoryLocation;

    [Test, Order(0)]
    public async Task Should_upload_ing_data()
    {
        // Arrange
        var content = await CreateContent("files/test_1.csv", "transactionsFile");

        // Act
        var response = await Client.PostAsync("/upload-ing-data", content);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created));
            Assert.That(response.Headers.Location, Is.Not.Null);
        });

        _accountLocation = response.Headers.Location.ToString();
    }

    [Test, Order(1)]
    public async Task Should_read_account_info()
    {
        // Act
        var account = await Client.GetFromJsonAsync<Account>(_accountLocation);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(account, Is.Not.Null);
            Assert.That(account.Info.Name, Is.EqualTo("Main account"));
        });
    }

    [Test, Order(2)]
    public async Task Should_read_trasactions()
    {
        // Act
        var transactions = await Client.GetFromJsonAsync<Transaction[]>($"{_accountLocation}/transactions");

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(transactions, Is.Not.Null);
            Assert.That(transactions, Has.Length.EqualTo(5));
        });
    }

    [Test, Order(3)]
    public async Task Should_append_transactions()
    {
        // Arrange
        var content = await CreateContent("files/test_2.csv", "transactionsFile");

        // Act
        await Client.PostAsync("/upload-ing-data", content);
        var transactions = await Client.GetFromJsonAsync<Transaction[]>($"{_accountLocation}/transactions");

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(transactions, Is.Not.Null);
            Assert.That(transactions, Has.Length.EqualTo(10));
        });
    }

    [Test, Order(4)]
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

    [Test, Order(5)]
    public async Task Should_read_all_categories() {
        // Act
        var categories = await Client.GetFromJsonAsync<Category[]>("/categories");

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(categories, Is.Not.Null);
            Assert.That(categories, Has.Length.EqualTo(1));
        });
     }

    [Test, Order(6)]
    public async Task Should_delete_category() {
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

    private static async Task<MultipartFormDataContent> CreateContent(string filePath, string name)
    {
        var data = await File.ReadAllBytesAsync(filePath);
        return new() { { new ByteArrayContent(data), name, name } };
    }
}
