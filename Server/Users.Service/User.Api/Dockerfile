FROM mcr.microsoft.com/dotnet/aspnet:5.0-focal AS base
WORKDIR /app
EXPOSE 80/tcp

FROM mcr.microsoft.com/dotnet/sdk:5.0-focal AS build
WORKDIR /src
COPY ["User.Api/User.Api.csproj", "User.Api/"]
COPY ["User.Services/User.Services.csproj", "User.Services/"]
COPY ["User.DataAccess/User.DataAccess.csproj", "User.DataAccess/"]
COPY ["User.Models/User.Models.csproj", "User.Models/"]

RUN dotnet restore "User.Api/User.Api.csproj"
COPY . .
WORKDIR "/src/User.Api"
RUN dotnet build "User.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "User.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "User.Api.dll"]