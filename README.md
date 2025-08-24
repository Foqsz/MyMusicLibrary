# MyMusicLibrary 🎵

![.NET](https://img.shields.io/badge/.NET-8.0-blue?logo=dotnet)
![ASP.NET Core](https://img.shields.io/badge/ASP.NET%20Core-Backend-purple?logo=dotnet)
![Google OAuth](https://img.shields.io/badge/Auth-Google-red?logo=google)
![Build](https://img.shields.io/badge/build-passing-brightgreen)
![Tests](https://img.shields.io/badge/tests-100%25-success)
![License](https://img.shields.io/badge/license-MIT-yellow)
![Contributions](https://img.shields.io/badge/contributions-welcome-orange)

**MyMusicLibrary** é um projeto backend desenvolvido em **ASP.NET Core 9.0**,  
com **autenticação via Google OAuth2**, suporte a testes unitários, mocks e geração de dados fake.  

---

## 📌 Funcionalidades

- 🎶 API para gerenciamento de músicas, álbuns e playlists(em breve).
- 🔍 Busca por nome, gênero ou artista  
- 🔑 Autenticação e autorização via **Google OAuth2**  
- 📂 Organização em biblioteca pessoal  
- 🧪 Testes unitários com **Moq** e **FluentAssertions**  
- 🧑‍💻 Geração de dados fake com **Bogus**  

---

## 🛠️ Tecnologias Utilizadas

- **ASP.NET Core 8.0** – API REST  
- **Google OAuth2** – autenticação e login  
- **Bogus** – geração de dados fake para testes  
- **Moq** – criação de mocks  
- **FluentAssertions** – assertions legíveis nos testes  
- **xUnit / NUnit** – framework de testes (adicione o que você estiver usando)  

---

## 🚀 Como Executar

### Pré-requisitos
- [.NET SDK](https://dotnet.microsoft.com/en-us/download) (8.0 ou superior)  
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
