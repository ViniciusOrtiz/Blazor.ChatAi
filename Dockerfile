# Stage 1: Build .NET and Node.js
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app

# Install Node.js 20+ to support the correct npm version
RUN apt-get update && apt-get install -y curl && \
    curl -fsSL https://deb.nodesource.com/setup_20.x | bash - && \
    apt-get install -y nodejs

# Copy project files before restoring dependencies
COPY *.sln ./
COPY App/*.csproj App/
COPY Application/*.csproj Application/
COPY Domain/*.csproj Domain/
COPY Data/*.csproj Data/
COPY Infrastructure/*.csproj Infrastructure/

# Restore .NET packages
RUN dotnet restore App/App.csproj

# Copy the full source code after restoring dependencies
COPY . ./

# 📌 Adjustment here: Setting the correct working directory where package.json is located
WORKDIR /app/App

# Install npm dependencies and run the build process (if necessary)
RUN npm install
RUN npm run build

# Return to the main directory and publish the .NET application in Release mode
WORKDIR /app
RUN dotnet publish App/App.csproj -c Release -o /app/out

# Stage 2: Runtime for ASP.NET Core
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app

# Copy the published .NET application from the build stage
COPY --from=build /app/out ./

# Expose the application port
EXPOSE 8000

# Start the application
ENTRYPOINT ["dotnet", "App.dll"]
