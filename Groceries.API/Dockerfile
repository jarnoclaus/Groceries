FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# NOTE: This path is relative to the repo root
COPY ["Groceries.API/Groceries.API.csproj", "Groceries.API/"]
RUN dotnet restore "Groceries.API/Groceries.API.csproj"

COPY . .
WORKDIR "/src/Groceries.API"
RUN dotnet build "Groceries.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Groceries.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Groceries.API.dll"]
