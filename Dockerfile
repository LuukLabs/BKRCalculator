# Use the official .NET SDK image as a base image
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build-env

# Set the working directory inside the container
WORKDIR /app

# Copy the .csproj and restore as distinct layers
COPY ["BKRCalculator/*.csproj", "BKRCalculator/"]
COPY ["BKRCalculatorApi/*.csproj", "BKRCalculatorApi/"]

RUN dotnet restore "BKRCalculatorApi/BKRCalculatorApi.csproj"

# Copy the remaining files
COPY . ./

# Build the application
RUN dotnet publish -c Release -o out

# Build the runtime image
FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "BKRCalculatorApi.dll"]
