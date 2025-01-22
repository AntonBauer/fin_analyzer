using System.Reflection;
using FinAnalyser.DataAccess.Extensions;
using FinAnalyzer.Api.UseCases.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi("fin-analyzer");
builder.Services.AddDataAccess(builder.Configuration)
                .AddUseCasesServices();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
    app.MapOpenApi()
       .CacheOutput();

app.UseHttpsRedirection();
app.AddUseCasesEndpoints();

if (!IsOpenApiGenerator())
    await app.ApplyMigrations();

app.Run();

static bool IsOpenApiGenerator() =>
    Assembly.GetEntryAssembly()?.GetName().Name == "GetDocument.Insider";