FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/InventoryAPI.API/InventoryAPI.API.csproj", "src/InventoryAPI.API/"]
COPY ["src/InventoryAPI.Domain/InventoryAPI.Domain.csproj", "src/InventoryAPI.Domain/"]
COPY ["src/InventoryAPI.Entities/InventoryAPI.Entities.csproj", "src/InventoryAPI.Entities/"]
COPY ["src/InventoryAPI.Infrastructure/InventoryAPI.Infrastructure.csproj", "src/InventoryAPI.Infrastructure/"]
RUN dotnet restore "src/InventoryAPI.API/InventoryAPI.API.csproj"
COPY . .
WORKDIR "/src/src/InventoryAPI.API"
RUN dotnet build "./InventoryAPI.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./InventoryAPI.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "InventoryAPI.API.dll"]
