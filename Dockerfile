# Etapa 1: Build
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build-env
WORKDIR /app

# Copia todo o código
COPY src/ .

# Define o diretório do projeto principal
WORKDIR Backend/MyMusicLibrary.API

# Restaura e publica apenas este projeto
RUN dotnet restore
RUN dotnet publish -c Release -o /app/out

# Etapa 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app

# Copia a saída do publish
COPY --from=build-env /app/out .

# Define o entrypoint da aplicação
ENTRYPOINT ["dotnet", "MyMusicLibrary.API.dll"]
