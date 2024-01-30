# FROM mcr.microsoft.com/dotnet/aspnet:8.0
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /App

RUN dotnet tool install --global dotnet-ef
ENV PATH="$PATH:/root/.dotnet/tools"

# Copy everything
COPY . ./
# Restore as distinct layers
RUN dotnet restore BackApi.csproj
# Build and publish a release
RUN dotnet publish BackApi.csproj -c Release -o out

# Build runtime image
WORKDIR /App

# ENTRYPOINT ["dotnet", "/App/bin/Release/net8.0/BackApi.dll"]
ENTRYPOINT ["bash", "/App/start.sh"]
