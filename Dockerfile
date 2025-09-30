# Etapa 1: Build
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build

WORKDIR /app

# Copiar a solução e os projetos necessários
COPY MyMusicLibrary.sln .
COPY src/Backend/MyMusicLibrary.API/MyMusicLibrary.API.csproj src/Backend/MyMusicLibrary.API/
COPY src/Backend/MyMusicLibrary.Application/MyMusicLibrary.Application.csproj src/Backend/MyMusicLibrary.Application/
COPY src/Backend/MyMusicLibrary.Domain/MyMusicLibrary.Domain.csproj src/Backend/MyMusicLibrary.Domain/
COPY src/Backend/MyMusicLibrary.Infrastructure/MyMusicLibrary.Infrastructure.csproj src/Backend/MyMusicLibrary.Infrastructure/
COPY src/Shared/MyMusicLibrary.Communication/MyMusicLibrary.Communication.csproj src/Shared/MyMusicLibrary.Communication/
COPY src/Shared/MyMusicLibrary.Exceptions/MyMusicLibrary.Exceptions.csproj src/Shared/MyMusicLibrary.Exceptions/

# Restaurar dependências (somente os projetos de produção)
RUN dotnet restore MyMusicLibrary.sln

# Copiar todo o código-fonte
COPY src/ src/

# Publicar o projeto para produção
WORKDIR /app/src/Backend/MyMusicLibrary.API
RUN dotnet publish -c Release -o /app/publish

# Etapa 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:9.0

WORKDIR /app

# Copiar a saída publicada da etapa de build
COPY --from=build /app/publish .

# Definir a porta e o entrypoint
EXPOSE 80
ENTRYPOINT ["dotnet", "MyMusicLibrary.API.dll"]
