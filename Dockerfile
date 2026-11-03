FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ProductManagement.sln .

COPY NuGet.Docker.Config /tmp/NuGet.Config

COPY src/ProductManagement.API/ProductManagement.API.csproj src/ProductManagement.API/
COPY src/ProductManagement.Application/ProductManagement.Application.csproj src/ProductManagement.Application/
COPY src/ProductManagement.Domain/ProductManagement.Domain.csproj src/ProductManagement.Domain/
COPY src/ProductManagement.Infrastructure/ProductManagement.Infrastructure.csproj src/ProductManagement.Infrastructure/

COPY test/ProductManagement.UnitTests/ProductManagement.UnitTests.csproj test/ProductManagement.UnitTests/
COPY test/ProductManagement.IntegrationTests/ProductManagement.IntegrationTests.csproj test/ProductManagement.IntegrationTests/

RUN dotnet restore ProductManagement.sln --configfile /tmp/NuGet.Config

COPY . .

RUN dotnet publish src/ProductManagement.API/ProductManagement.API.csproj \
    -c Release \
    -o /app/publish \
    --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

COPY --from=build /app/publish .

EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080

ENTRYPOINT ["dotnet", "ProductManagement.API.dll"]