# Build stage
FROM mcr.microsoft.com/dotnet/sdk:10.0-alpine AS build
WORKDIR /src

# Copy solution and project files
COPY BIS.sln .
COPY src/ src/
COPY tests/ tests/
RUN dotnet restore

# Copy all source files
COPY . .

# Build the specific project (passed as ARG)
ARG PROJECT=AuthService
RUN dotnet build "src/${PROJECT}/${PROJECT}.csproj" -c Release -o /app/build

# Publish stage
FROM build AS publish
ARG PROJECT=AuthService
RUN dotnet publish "src/${PROJECT}/${PROJECT}.csproj" -c Release -o /app/publish

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:10.0-alpine AS final
WORKDIR /app
COPY --from=publish /app/publish .

ARG PROJECT=AuthService
ENV PROJECT_NAME=${PROJECT}

EXPOSE 8080
ENTRYPOINT ["sh", "-c", "dotnet BIS.$PROJECT_NAME.dll"]