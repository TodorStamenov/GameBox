FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80/tcp

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["GameBox.Web.API/GameBox.Bootstrap/GameBox.Bootstrap.csproj", "GameBox.Bootstrap/"]
COPY ["GameBox.Web.API/GameBox.Api/GameBox.Api.csproj", "GameBox.Api/"]
COPY ["GameBox.Web.API/GameBox.Application/GameBox.Application.csproj", "GameBox.Application/"]
COPY ["GameBox.Web.API/GameBox.Domain/GameBox.Domain.csproj", "GameBox.Domain/"]
COPY ["GameBox.Web.API/GameBox.Persistence/GameBox.Persistence.csproj", "GameBox.Persistence/"]
COPY ["GameBox.Web.API/GameBox.Infrastructure/GameBox.Infrastructure.csproj", "GameBox.Infrastructure/"]

RUN dotnet restore "GameBox.Bootstrap/GameBox.Bootstrap.csproj"
WORKDIR "/src/GameBox.Bootstrap"
COPY . .
RUN dotnet build "GameBox.Bootstrap.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "GameBox.Bootstrap.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
CMD ["dotnet", "GameBox.Bootstrap.dll"]