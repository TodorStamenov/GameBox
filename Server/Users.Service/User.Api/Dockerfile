FROM mcr.microsoft.com/dotnet/aspnet:5.0-focal AS base
WORKDIR /app
EXPOSE 80/tcp

FROM mcr.microsoft.com/dotnet/sdk:5.0-focal AS build
WORKDIR /src
COPY ["Users.Service/User.Api/User.Api.csproj", "Users.Service/User.Api/"]
COPY ["Users.Service/User.Services/User.Services.csproj", "Users.Service/User.Services/"]
COPY ["Users.Service/User.DataAccess/User.DataAccess.csproj", "Users.Service/User.DataAccess/"]
COPY ["Users.Service/User.Models/User.Models.csproj", "Users.Service/User.Models/"]
COPY ["Common/Message.DataAccess/Message.DataAccess.csproj", "Users.Service/Message.DataAccess/"]

RUN dotnet restore "Users.Service/User.Api/User.Api.csproj"
COPY . .
WORKDIR "/src/Users.Service/User.Api"
RUN dotnet build "User.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "User.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "User.Api.dll"]