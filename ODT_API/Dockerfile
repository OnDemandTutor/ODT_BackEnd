#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
ENV ASPNETCORE_ENVIRONMENT=Development

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["ODT_API/ODT_API.csproj", "ODT_API/"]
COPY ["ODT_Model/ODT_Model.csproj", "ODT_Model/"]
COPY ["ODT_Repository/ODT_Repository.csproj", "ODT_Repository/"]
COPY ["ODT_Service/ODT_Service.csproj", "ODT_Service/"]
COPY ["Tools/Tools.csproj", "Tools/"]
RUN dotnet restore "./ODT_API/ODT_API.csproj"
COPY . .
WORKDIR "/src/ODT_API"
RUN dotnet build "./ODT_API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./ODT_API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENV ASPNETCORE_URLS="http://+:8080"
ENTRYPOINT ["dotnet", "ODT_API.dll"]