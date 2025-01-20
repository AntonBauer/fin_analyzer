using FinAnalyzer.Api.UseCases.UploadIngData;

namespace FinAnalyzer.Api.UseCases;

public static class WebApplicationExtensions
{
    public static WebApplication AddUseCasesEndpoints(this WebApplication app)
    {
        app.AddLoadIngDataEndpoints();

        return app;
    }
}