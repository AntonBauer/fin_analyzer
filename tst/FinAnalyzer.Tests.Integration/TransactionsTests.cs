namespace FinAnalyzer.Tests.Integration;

[TestFixture]
internal sealed class TransactionsTests : IntegrationTestBase
{
    [Test, Order(0)]
    public async Task Should_load_ing_data()
    {
        // Arrange

        // Act
        await Task.Delay(2000);

        // Assert
        Assert.Fail();
    }

    [Test, Order(1)]
    public async Task Should_read_transactions()
    {
        // Arrange

        // Act
        await Task.Delay(3000);

        // Assert
        Assert.Fail();
    }
}
