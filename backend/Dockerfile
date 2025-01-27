FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /source

COPY FinAnalyzer.sln .

COPY src/FinAnalyzer.Api/FinAnalyzer.Api.csproj ./src/FinAnalyzer.Api/
COPY src/FinAnalyzer.DataAccess/FinAnalyser.DataAccess.csproj ./src/FinAnalyzer.DataAccess/
COPY src/FinAnalyzer.Domain/FinAnalyzer.Domain.csproj ./src/FinAnalyzer.Domain/
COPY src/FinAnalyzer.IngData/FinAnalyzer.IngData.csproj ./src/FinAnalyzer.IngData/

COPY tst/FinAnalyzer.Tests.Integration/FinAnalyzer.Tests.Integration.csproj ./tst/FinAnalyzer.Tests.Integration/
COPY tst/FinAnalyzer.Tests.Unit/FinAnalyzer.Tests.Unit.csproj ./tst/FinAnalyzer.Tests.Unit/

RUN dotnet restore

COPY . .

RUN dotnet publish ./src/FinAnalyzer.Api/FinAnalyzer.Api.csproj -c release -o /app

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
COPY --from=build /app .
EXPOSE 80
ENTRYPOINT ["dotnet", "FinAnalyzer.Api.dll"]