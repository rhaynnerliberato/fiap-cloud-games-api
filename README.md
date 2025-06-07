# Fiap Cloud Games - Documentação do Projeto

## 🌟 Objetivo do Projeto

Este é um sistema de backend RESTful voltado para uma plataforma de gerenciamento de jogos na nuvem, chamada **Fiap Cloud Games**. A API permite operações de CRUD para usuários e jogos, autenticação de usuário e funcionalidades adicionais como avaliação de jogos.

Foi desenvolvida em .NET 8 com MongoDB, utilizando boas práticas como injeção de dependência, TDD e arquitetura em camadas (Controller, Service, Repository, Domain).

---

## 🚀 Tecnologias Utilizadas

* .NET 8
* MongoDB
* Migrations
* AutoMapper
* Autenticação via JWT
* xUnit
* Moq
* Swagger (Swashbuckle)
* Github Actions (CI/CD)

---

## 📂 Estrutura do Projeto

```
fiap-cloud-games-api/
|-- fiap-cloud-games.API/
|   |-- Controllers/
|   |-- Middlewares/
|   |-- Program.cs
|   |-- appsettings.json

|-- fiap-cloud-games.Application/
|   |-- AutoMapper/
|   |-- DTOs/
|   |-- Services/

|-- fiap-cloud-games.Domain/
|   |-- Entities/
|   |-- Enums/
|   |-- Interfaces/
|   |-- ValueObjects/
|
|-- fiap-cloud-games.Infrastructure/
|   |-- Configurations/
|   |-- Context/
|   |-- Migrations/
|   |-- Repositories/
|
|-- fiap-cloud-games.Tests/
|   |-- Services/
|
|-- fiap-cloud-games.sln
```

---

## 🔧 Como Executar o Projeto

### 1. Clone o repositório

```bash
git clone https://github.com/seu-usuario/fiap-cloud-games-api.git
cd fiap-cloud-games-api
```

### 2. Configure o `appsettings.json`

```json
{
  "MongoDbSettings": {
    "ConnectionString": "mongodb://localhost:27017",
    "DatabaseName": "FiapCloudGamesDB"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*"
}
```

### 3. Execute a aplicação

```bash
dotnet run
```

A aplicação estará disponível em: `https://localhost:5001`

### 4. Acesse a documentação Swagger

```
https://localhost:5001/swagger
```

---

## 📄 Endpoints Disponíveis

### Login

* POST `/api/auth/login` - Login/autenticação
* GET `/api/auth/erro` - Testa middleware de tratamento global de erros

### Usuários

* POST `/api/usuarios/cadastrar` - Cadastrar novo usuário
* GET `/api/usuarios/listar` - Listar todos os usuários
* GET `/api/usuarios/buscar-usuario/{id}` - Buscar por ID
* PUT `/api/usuarios/alterar-usuario/{id}` - Atualizar dados
* DELETE `/api/usuarios/deletar-usuario/{id}` - Deletar usuário

### Jogos

* POST `/api/jogos/cadastrar` - Cadastrar novo jogo
* GET `/api/jogos/listar` - Listar todos os jogos
* GET `/api/jogos/buscar-jogo/{id}` - Buscar por ID
* PUT `/api/jogos/alterar-jogo/{id}` - Atualizar dados
* DELETE `/api/jogos/deletar-jogo/{id}` - Deletar jogo
* POST `/api/jogos/{id}/promocao` - Lança valor promocional

---

## ✅ Testes Unitários

Os testes estão localizados em:

```
Tests/Services/
```

Exemplos de testes implementados:

* `UsuarioServiceTests`

  * CadastrarAsync\_ValidRequest\_ReturnsResponse
  * AutenticarAsync\_UsuarioExiste\_RetornaUsuario
  * ObterPorIdAsync\_UsuarioExiste\_RetornaUsuarioResponse
  * AtualizarAsync\_UsuarioNaoExiste\_RetornaNull
  * DeletarAsync\_UsuarioExiste\_DeletaRetornaTrue

* `JogoServiceTests`

  * CadastrarAsync\_DeveRetornarJogo
  * ObterTodosAsync\_DeveRetornarListaJogos

Para rodar os testes:

```bash
dotnet test
```

---

## 🚧 Melhorias Futuras

* Filtros de jogos por preço, categoria ou avaliação
* Upload de imagem para capa do jogo
* Painel admin

---

## 👨‍💻 Desenvolvedor

* Rhaynner Liberato *