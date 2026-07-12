# Сборка
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build

WORKDIR /src

COPY API.slnx .

COPY Presentation/Presentation.csproj Presentation/
COPY Application/Application.csproj Application/
COPY CompositionRoot/CompositionRoot.csproj CompositionRoot/
COPY Contracts/Contracts.csproj Contracts/
COPY Domain/Domain.csproj Domain/
COPY Infrastructure/Infrastructure.csproj Infrastructure/
COPY Shared/Shared.csproj Shared/

RUN dotnet restore API.slnx

COPY . .

WORKDIR /src/Presentation

RUN dotnet publish -c Release -o /app/publish


# Runtime
FROM mcr.microsoft.com/dotnet/aspnet:10.0

WORKDIR /app

COPY --from=build /app/publish .

EXPOSE 8080

ENTRYPOINT ["dotnet", "Presentation.dll"]