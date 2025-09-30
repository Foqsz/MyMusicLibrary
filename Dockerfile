# Build
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copia a solução para /src
COPY MyMusicLibrary.sln .
COPY src/Backend/MyMusicLibrary.API/MyMusicLibrary.API.csproj src/Backend/MyMusicLibrary.API/
COPY src/Backend/MyMusicLibrary.Application/MyMusicLibrary.Application.csproj src/Backend/MyMusicLibrary.Application/
COPY src/Backend/MyMusicLibrary.Domain/MyMusicLibrary.Domain.csproj src/Backend/MyMusicLibrary.Domain/
COPY src/Backend/MyMusicLibrary.Infrastructure/MyMusicLibrary.Infrastructure.csproj src/Backend/MyMusicLibrary.Infrastructure/
COPY src/Shared/MyMusicLibrary.Communication/MyMusicLibrary.Communication.csproj src/Shared/MyMusicLibrary.Communication/
COPY src/Shared/MyMusicLibrary.Exceptions/MyMusicLibrary.Exceptions.csproj src/Shared/MyMusicLibrary.Exceptions/

RUN dotnet restore MyMusicLibrary.sln

COPY src/ src/
RUN dotnet publish src/Backend/MyMusicLibrary.API/MyMusicLibrary.API.csproj -c Release -o /app/publish

# Runtime
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080
ENTRYPOINT ["dotnet", "MyMusicLibrary.API.dll"]
