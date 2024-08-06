
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["ComputerStore_MSSQL.csproj", "."]
RUN dotnet restore "./ComputerStore_MSSQL.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "./ComputerStore_MSSQL.csproj" -c %BUILD_CONFIGURATION% -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./ComputerStore_MSSQL.csproj" -c %BUILD_CONFIGURATION% -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
COPY ComputerStore/appsettings.json
ENTRYPOINT ["dotnet", "ComputerStore_MSSQL.dll"]