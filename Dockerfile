# Use official .NET SDK image for building the application
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /app

# Copy project files and restore dependencies
COPY *.sln ./
COPY src/ ./src/
RUN dotnet restore

# Build the application
RUN dotnet publish -c Release -o /out

# Use a lightweight runtime image for running the application
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build /out ./

# Expose application port
EXPOSE 80
EXPOSE 443

# Run the application
ENTRYPOINT ["dotnet", "MyProject.dll"]
