# https://hub.docker.com/_/microsoft-dotnet
FROM --platform=linux/amd64 mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /app

# copy csproj and restore as distinct layers
# COPY *.sln .
COPY API/*.csproj ./
# COPY API.Tests/*.csproj ./API.Tests/
RUN dotnet restore

# copy everything else and build app
COPY API/. ./
# COPY API.Tests/. ./API.Tests/
RUN dotnet publish -c Release -o out

# final stage/image
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build /app/out .
EXPOSE 80
ENTRYPOINT ["dotnet", "API.dll"]