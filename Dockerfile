# Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copier solution et projet
COPY *.sln ./
COPY *.csproj ./

# Restore les packages
RUN dotnet restore

# Copier tout le code
COPY . ./

# Publier le projet
RUN dotnet publish -c Release -o /out

# Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /out .
EXPOSE 5146
ENTRYPOINT ["dotnet", "Sim_Forum.dll"]