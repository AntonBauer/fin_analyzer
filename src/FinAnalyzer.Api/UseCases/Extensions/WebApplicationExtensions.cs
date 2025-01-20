namespace FinAnalyzer.Api.UseCases.Extensions;

public static class WebApplicationExtensions
{
    public static WebApplication AddUseCasesEndpoints(this WebApplication app)
    {
        app.AddLoadIngDataEndpoints()
           .AddReadAccountEndpoints();

        return app;
    }
}