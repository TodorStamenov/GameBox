FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /src

COPY GameBox.Admin.UI.csproj .
RUN dotnet restore "GameBox.Admin.UI.csproj"

COPY . .
RUN dotnet build "GameBox.Admin.UI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "GameBox.Admin.UI.csproj" -c Release -o /app/publish

FROM nginx:1.25-alpine AS final
WORKDIR /usr/share/nginx/html
COPY --from=publish /app/publish/wwwroot .
COPY nginx.conf /etc/nginx/nginx.conf