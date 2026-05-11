FROM mcr.microsoft.com/dotnet/runtime:10.0 AS base
USER $APP_UID
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["tests/InventoryAPI.Tests/InventoryAPI.Tests.csproj", "tests/InventoryAPI.Tests/"]
COPY ["src/InventoryAPI.Domain/InventoryAPI.Domain.csproj", "src/InventoryAPI.Domain/"]
COPY ["src/InventoryAPI.Entities/InventoryAPI.Entities.csproj", "src/InventoryAPI.Entities/"]
RUN dotnet restore "tests/InventoryAPI.Tests/InventoryAPI.Tests.csproj"
COPY . .
WORKDIR "/src/tests/InventoryAPI.Tests"
RUN dotnet build "./InventoryAPI.Tests.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./InventoryAPI.Tests.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "InventoryAPI.Tests.dll"]
