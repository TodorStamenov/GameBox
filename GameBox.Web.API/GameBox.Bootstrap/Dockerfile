FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80/tcp

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["GameBox.Bootstrap/GameBox.Bootstrap.csproj", "GameBox.Bootstrap/"]
COPY ["GameBox.Api/GameBox.Api.csproj", "GameBox.Api/"]
COPY ["GameBox.Application/GameBox.Application.csproj", "GameBox.Application/"]
COPY ["GameBox.Domain/GameBox.Domain.csproj", "GameBox.Domain/"]
COPY ["GameBox.Persistence/GameBox.Persistence.csproj", "GameBox.Persistence/"]
COPY ["GameBox.Infrastructure/GameBox.Infrastructure.csproj", "GameBox.Infrastructure/"]

RUN dotnet restore "GameBox.Bootstrap/GameBox.Bootstrap.csproj"
COPY . .
WORKDIR "/src/GameBox.Bootstrap"
RUN dotnet build "GameBox.Bootstrap.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "GameBox.Bootstrap.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GameBox.Bootstrap.dll"]