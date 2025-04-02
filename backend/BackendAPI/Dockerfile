# Use .NET 9.0 SDK to build the app
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app

# Copy everything and build
COPY . .  
RUN dotnet publish -c Release -o out

# Use .NET 9.0 Runtime image for the app
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app

# Copy the build output to the runtime container
COPY --from=build /app/out .

# Expose port
EXPOSE 8080

# Set the environment to Development (so Swagger is visible)
ENV ASPNETCORE_ENVIRONMENT=Development

# Run the app
ENTRYPOINT ["dotnet", "BackendAPI.dll"]
