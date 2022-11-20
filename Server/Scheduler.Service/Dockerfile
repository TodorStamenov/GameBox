FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY Scheduler.Service.csproj .

RUN dotnet restore Scheduler.Service.csproj
COPY . .
WORKDIR /src
RUN dotnet build "Scheduler.Service.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Scheduler.Service.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Scheduler.Service.dll"]