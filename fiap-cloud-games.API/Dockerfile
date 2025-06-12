# Etapa 1: Build.
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY . .

RUN dotnet restore ./fiap-cloud-games.API/fiap-cloud-games.API.csproj
RUN dotnet publish ./fiap-cloud-games.API/fiap-cloud-games.API.csproj -c Release -o /app/publish

# Etapa 2: Runtime.
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .

EXPOSE 80
ENTRYPOINT ["dotnet", "Fiap.Cloud.Games.Api.dll"]
