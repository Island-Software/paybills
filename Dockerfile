# Build Stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /source
COPY /Paybills.API .
RUN dotnet restore "./Paybills.API.csproj" --disable-parallel
RUN dotnet publish "./Paybills.API.csproj" -c release -o /app --no-restore

# Serve Stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app ./

# Set environment variable to bind to port 5000
ENV ASPNETCORE_URLS=http://+:5000

EXPOSE 5000

ENTRYPOINT [ "dotnet", "Paybills.API.dll" ]