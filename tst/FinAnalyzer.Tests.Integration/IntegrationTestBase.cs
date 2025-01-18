
using FinAnalyzer.Tests.Integration;

internal class IntegrationTestBase
{
    private FinAnalyzeApplicationFactory _appFactory;
    protected HttpClient Client;

    [OneTimeSetUp]
    public async Task SetUp()
    {
        _appFactory = new FinAnalyzeApplicationFactory();
        await _appFactory.Initialize();

        Client = _appFactory.CreateClient();
    }

    [OneTimeTearDown]
    public async Task TearDown()
    {
        Client.Dispose();
        await _appFactory.DisposeAsync();
    }
}