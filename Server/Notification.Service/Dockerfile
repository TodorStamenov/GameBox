FROM mcr.microsoft.com/dotnet/aspnet:5.0-focal AS base
WORKDIR /app
EXPOSE 80/tcp

FROM mcr.microsoft.com/dotnet/sdk:5.0-focal AS build
WORKDIR /src
COPY Notification.Service.csproj .

RUN dotnet restore Notification.Service.csproj
COPY . .
WORKDIR /src
RUN dotnet build "Notification.Service.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Notification.Service.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Notification.Service.dll"]