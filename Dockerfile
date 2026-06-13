# Build stage
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Copy solution and project files first for layer caching
COPY TravelBuddy.sln* TravelBuddy.slnx* ./
COPY TravelBuddy/TravelBuddy.csproj TravelBuddy/
COPY TravelBuddy.Data/TravelBuddy.Data.csproj TravelBuddy.Data/
COPY TravelBuddy.Data.Models/TravelBuddy.Data.Models.csproj TravelBuddy.Data.Models/
COPY TravelBuddy.Services.Core/TravelBuddy.Services.Core.csproj TravelBuddy.Services.Core/
COPY TravelBuddy.ViewModels/TravelBuddy.ViewModels.csproj TravelBuddy.ViewModels/
COPY TravelBuddy.GCommon/TravelBuddy.GCommon.csproj TravelBuddy.GCommon/
COPY TravelBuddy.Infrastructure/TravelBuddy.Infrastructure.csproj TravelBuddy.Infrastructure/

RUN dotnet restore TravelBuddy/TravelBuddy.csproj

# Copy full source and publish
COPY . .
RUN dotnet publish TravelBuddy/TravelBuddy.csproj -c Release -o /app/publish --no-restore

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime
WORKDIR /app

COPY --from=build /app/publish .

# Railway injects PORT at runtime; default to 8080 for local docker run
ENV ASPNETCORE_URLS=http://+:${PORT:-8080}
ENV ASPNETCORE_ENVIRONMENT=Production

EXPOSE 8080

ENTRYPOINT ["dotnet", "TravelBuddy.dll"]
