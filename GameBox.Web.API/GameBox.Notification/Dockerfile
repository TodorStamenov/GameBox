FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80/tcp

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["GameBox.Notification/GameBox.Notification.csproj", "GameBox.Notification/"]

RUN dotnet restore "GameBox.Notification/GameBox.Notification.csproj"
COPY . .
WORKDIR "/src/GameBox.Notification"
RUN dotnet build "GameBox.Notification.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "GameBox.Notification.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GameBox.Notification.dll"]