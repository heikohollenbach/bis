# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0-alpine AS build
WORKDIR /src

# Copy solution and project files
COPY ["BIS.sln", "."]
COPY ["src/Common/Common.csproj", "src/Common/"]
COPY ["src/Domain/Domain.csproj", "src/Domain/"]
COPY ["src/Data/Data.csproj", "src/Data/"]
COPY ["src/AuthService/AuthService.csproj", "src/AuthService/"]
COPY ["src/PlayerService/PlayerService.csproj", "src/PlayerService/"]
COPY ["src/WorldService/WorldService.csproj", "src/WorldService/"]
COPY ["src/ChatService/ChatService.csproj", "src/ChatService/"]

# Restore all projects
RUN dotnet restore "BIS.sln"

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
FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine AS final
WORKDIR /app
COPY --from=publish /app/publish .

ARG PROJECT=AuthService
ENV PROJECT_NAME=${PROJECT}

EXPOSE 8080
ENTRYPOINT ["dotnet", "AuthService.dll"]
