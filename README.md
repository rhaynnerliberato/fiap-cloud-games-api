# Fiap Cloud Games - Documentação do Projeto

## 🌟 Objetivo do Projeto

Este é um sistema de backend RESTful voltado para uma plataforma de gerenciamento de jogos na nuvem, chamada **Fiap Cloud Games**. A API permite operações de CRUD para usuários e jogos, autenticação de usuário e funcionalidades adicionais como avaliação de jogos.

Foi desenvolvida em .NET 6 com MongoDB, utilizando boas práticas como injeção de dependência, TDD e arquitetura em camadas (Controller, Service, Repository, Domain).

---

## 🚀 Tecnologias Utilizadas

* .NET 6
* MongoDB
* AutoMapper
* xUnit
* Moq
* Swagger (Swashbuckle)
* Github Actions (CI/CD)
* Docker

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

### Usuários

* POST `/api/usuarios` - Cadastrar novo usuário
* GET `/api/usuarios` - Listar todos os usuários
* GET `/api/usuarios/{id}` - Buscar por ID
* PUT `/api/usuarios/{id}` - Atualizar dados
* DELETE `/api/usuarios/{id}` - Deletar usuário
* POST `/api/usuarios/login` - Login/autenticação

### Jogos

* POST `/api/jogos` - Cadastrar novo jogo
* GET `/api/jogos` - Listar todos os jogos
* GET `/api/jogos/{id}` - Buscar por ID
* PUT `/api/jogos/{id}` - Atualizar dados
* DELETE `/api/jogos/{id}` - Deletar jogo
* POST `/api/jogos/{id}/avaliar` - Avaliar jogo (TDD)

---

## 🔮 TDD Aplicado

Implementamos TDD no módulo de **avaliação de jogo**, com o ciclo clássico:

1. Criamos o teste primeiro (`JogoServiceTests.AvaliarAsync_...`).
2. Implementamos o método `AvaliarAsync` no `JogoService`.
3. Garantimos que o teste passasse.
4. Refatoramos a lógica e confirmamos a manutenção dos testes.

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
  * AvaliarAsync\_JogoExiste\_AvaliacaoValida\_DeveSalvarNota

Para rodar os testes:

```bash
dotnet test
```

---

## 🚧 Melhorias Futuras

* Autenticação via JWT
* Filtros de jogos por preço, categoria ou avaliação
* Upload de imagem para capa do jogo
* Painel admin

---

## 👨‍💻 Desenvolvedor

*Rhaynner Liberato*