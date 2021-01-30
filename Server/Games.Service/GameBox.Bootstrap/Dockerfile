FROM mcr.microsoft.com/dotnet/aspnet:5.0-focal AS base
WORKDIR /app
EXPOSE 80/tcp

FROM mcr.microsoft.com/dotnet/sdk:5.0-focal AS build
WORKDIR /src
COPY ["GameBox.Bootstrap/GameBox.Bootstrap.csproj", "GameBox.Bootstrap/"]
COPY ["Presentation/GameBox.Api/GameBox.Api.csproj", "GameBox.Api/"]
COPY ["Core/GameBox.Application/GameBox.Application.csproj", "GameBox.Application/"]
COPY ["Core/GameBox.Domain/GameBox.Domain.csproj", "GameBox.Domain/"]
COPY ["Infrastructure/GameBox.Persistence/GameBox.Persistence.csproj", "GameBox.Persistence/"]
COPY ["Infrastructure/GameBox.Infrastructure/GameBox.Infrastructure.csproj", "GameBox.Infrastructure/"]

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