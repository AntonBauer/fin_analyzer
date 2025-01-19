﻿using System.Net;

namespace FinAnalyzer.Tests.Integration;

[TestFixture]
internal sealed class TransactionsTests : IntegrationTestBase
{
    [Test, Order(0)]
    public async Task Should_load_ing_data()
    {
        // Arrange
        var content = await CreateContent("files/test_1.csv", "transactionsFile");

        // Act
        var response = await Client.PostAsync("/load-ing-data", content);

        // Assert
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created));
    }

    [Test, Order(1)]
    public async Task Should_read_transactions()
    {
        // Arrange

        // Act

        // Assert
        Assert.Fail();
    }

    private static async Task<MultipartFormDataContent> CreateContent(string filePath, string name)
    {
        var data = await File.ReadAllBytesAsync(filePath);

        return new MultipartFormDataContent
        {
            { new ByteArrayContent(data), name }
        };
    }
}
