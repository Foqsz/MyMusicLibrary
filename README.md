# MyMusicLibrary 🎵
[![Quality Gate Status](https://sonarcloud.io/api/project_badges/measure?project=Foqsz_MyMusicLibrary&metric=alert_status)](https://sonarcloud.io/summary/new_code?id=Foqsz_MyMusicLibrary)
[![Bugs](https://sonarcloud.io/api/project_badges/measure?project=Foqsz_MyMusicLibrary&metric=bugs)](https://sonarcloud.io/summary/new_code?id=Foqsz_MyMusicLibrary)
[![Code Smells](https://sonarcloud.io/api/project_badges/measure?project=Foqsz_MyMusicLibrary&metric=code_smells)](https://sonarcloud.io/summary/new_code?id=Foqsz_MyMusicLibrary)
[![Coverage](https://sonarcloud.io/api/project_badges/measure?project=Foqsz_MyMusicLibrary&metric=coverage)](https://sonarcloud.io/summary/new_code?id=Foqsz_MyMusicLibrary)
[![Duplicated Lines (%)](https://sonarcloud.io/api/project_badges/measure?project=Foqsz_MyMusicLibrary&metric=duplicated_lines_density)](https://sonarcloud.io/summary/new_code?id=Foqsz_MyMusicLibrary)
[![Security Rating](https://sonarcloud.io/api/project_badges/measure?project=Foqsz_MyMusicLibrary&metric=security_rating)](https://sonarcloud.io/summary/new_code?id=Foqsz_MyMusicLibrary)
[![Vulnerabilities](https://sonarcloud.io/api/project_badges/measure?project=Foqsz_MyMusicLibrary&metric=vulnerabilities)](https://sonarcloud.io/summary/new_code?id=Foqsz_MyMusicLibrary)
[![Maintainability Rating](https://sonarcloud.io/api/project_badges/measure?project=Foqsz_MyMusicLibrary&metric=sqale_rating)](https://sonarcloud.io/summary/new_code?id=Foqsz_MyMusicLibrary)

![.NET](https://img.shields.io/badge/.NET-9.0-blue?logo=dotnet)
![ASP.NET Core](https://img.shields.io/badge/ASP.NET%20Core-Backend-purple?logo=dotnet)
![Google OAuth](https://img.shields.io/badge/Auth-Google-red?logo=google)
![License](https://img.shields.io/badge/license-MIT-yellow)
![Contributions](https://img.shields.io/badge/contributions-welcome-orange)

**MyMusicLibrary** é um projeto backend desenvolvido em **ASP.NET Core 9.0**,  
com **autenticação via Google OAuth2**, suporte a testes unitários, mocks e geração de dados fake.  

---

## 📌 Funcionalidades

- 🎶 API para gerenciamento de músicas, álbuns, artistas e playlists.
- 🔍 Busca por nome, gênero ou artista  
- 🔑 Autenticação e autorização via **Google OAuth2**  
- 📂 Organização em biblioteca pessoal  
- 🧪 Testes unitários com **Moq** e **FluentAssertions**  
- 🧑‍💻 Geração de dados fake com **Bogus**  

---

## 🛠️ Tecnologias Utilizadas

- **ASP.NET Core 9.0** – API REST  
- **Google OAuth2** – autenticação e login  
- **Bogus** – geração de dados fake para testes  
- **Moq** – criação de mocks  
- **FluentAssertions** – assertions legíveis nos testes  
- **xUnit / NUnit** – framework de testes (adicione o que você estiver usando)  
- **Amazon AWS S3** - Upload de músicas.
- **Tag Lib** - Extração dos dados do arquivo MP3.
- **Azure** - Deploy e Banco de Dados.
  
---

## 🚀 Como Executar

### Pré-requisitos
- [.NET SDK](https://dotnet.microsoft.com/en-us/download) (9.0 ou superior)  
- Conta de desenvolvedor do [Google Cloud Console](https://console.cloud.google.com/)  
  - Configure suas credenciais OAuth2 e adicione `ClientId` e `ClientSecret` no `appsettings.json`  

### Instalação
```bash
# Clone o repositório
git clone https://github.com/Foqsz/MyMusicLibrary.git
cd MyMusicLibrary

# Restaure as dependências
dotnet restore

# Compile
dotnet build

# Execute
dotnet run
