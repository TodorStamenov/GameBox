FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80/tcp
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["Games.Service/GameBox.Bootstrap/GameBox.Bootstrap.csproj", "Games.Service/GameBox.Bootstrap/"]
COPY ["Games.Service/Presentation/GameBox.Api/GameBox.Api.csproj", "Games.Service/GameBox.Api/"]
COPY ["Games.Service/Core/GameBox.Application/GameBox.Application.csproj", "Games.Service/GameBox.Application/"]
COPY ["Games.Service/Core/GameBox.Domain/GameBox.Domain.csproj", "Games.Service/GameBox.Domain/"]
COPY ["Games.Service/Infrastructure/GameBox.Persistence/GameBox.Persistence.csproj", "Games.Service/GameBox.Persistence/"]
COPY ["Games.Service/Infrastructure/GameBox.Infrastructure/GameBox.Infrastructure.csproj", "Games.Service/GameBox.Infrastructure/"]
COPY ["Common/Message.DataAccess/Message.DataAccess.csproj", "Games.Service/Message.DataAccess/"]

RUN dotnet restore "Games.Service/GameBox.Bootstrap/GameBox.Bootstrap.csproj"
COPY . .
WORKDIR "/src/Games.Service/GameBox.Bootstrap"
RUN dotnet build "GameBox.Bootstrap.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "GameBox.Bootstrap.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GameBox.Bootstrap.dll"]