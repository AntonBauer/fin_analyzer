using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Testcontainers.PostgreSql;

namespace FinAnalyzer.Tests.Integration;

internal sealed class FinAnalyzeApplicationFactory : IAsyncDisposable
{
    private readonly InternalFactory _appFactory = new();
    private readonly PostgreSqlContainer _postgres = new PostgreSqlBuilder().WithImage("postgres:15-alpine")
                                                                            .Build();


    public async Task Initialize()
    {
        await _postgres.StartAsync();
        _appFactory.DbConnectionString = _postgres.GetConnectionString();
    }

    public HttpClient CreateClient() => _appFactory.CreateClient();

    public async ValueTask DisposeAsync() => await _postgres.DisposeAsync();

    private sealed class InternalFactory : WebApplicationFactory<Program>
    {
        public string DbConnectionString { get; set; } = null!;

        public InternalFactory()
        {
            ClientOptions.AllowAutoRedirect = false;
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.UseSetting("ConnectionStrings:FinAnalyzer", DbConnectionString);
        }
    }
}