# Укажите базовый образ
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["ComputerStore/ComputerStore.csproj", "ComputerStore/"]
COPY ["ComputerStore_MSSQL/ComputerStore_MSSQL.csproj", "ComputerStore_MSSQL/"]
RUN dotnet restore "ComputerStore/ComputerStore.csproj"
COPY . .
WORKDIR "/src/ComputerStore"
RUN dotnet build "ComputerStore.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ComputerStore.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
COPY ComputerStore/appsettings.json ./appsettings.json  # добавляем источник и назначение для appsettings.json
ENTRYPOINT ["dotnet", "ComputerStore.dll"]