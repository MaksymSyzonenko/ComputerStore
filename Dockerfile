# Use the SDK image to build the application
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

# Copy the project files and restore dependencies
COPY ["ComputerStore/ComputerStore.csproj", "ComputerStore/"]
COPY ["ComputerStore_MSSQL/ComputerStore_MSSQL.csproj", "ComputerStore_MSSQL/"]
RUN dotnet restore "ComputerStore/ComputerStore.csproj"

# Copy the entire source code
COPY . .

# Build the project
WORKDIR "/src/ComputerStore"
RUN dotnet build "ComputerStore.csproj" -c Release -o /app/build

# Publish the project
FROM build AS publish
RUN dotnet publish "ComputerStore.csproj" -c Release -o /app/publish

# Use the runtime image to run the application
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

# Copy the published output from the build stage
COPY --from=publish /app/publish .
COPY ComputerStore/appsettings.json ./appsettings.json  # добавляем источник и назначение для appsettings.json

# Run the application
ENTRYPOINT ["dotnet", "ComputerStore.dll"]


# Run the application
ENTRYPOINT ["dotnet", "ComputerStore.dll"]
