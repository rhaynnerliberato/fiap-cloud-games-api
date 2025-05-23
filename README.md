# Fiap Cloud Games - Documentação do Projeto

## 🌟 Objetivo do Projeto

Este é um sistema de backend RESTful voltado para uma plataforma de gerenciamento de jogos na nuvem, chamada **FIAP Cloud Games**.
A API permite operações de CRUD para usuários e jogos, autenticação e autorização de usuário e funcionalidades adicionais como promoção de jogos.
Foi desenvolvida em .NET 8 com MongoDB, utilizando boas práticas como injeção de dependência, TDD e arquitetura DDD (Controller, Service, Repository, Domain).

---

## 🚀 Tecnologias Utilizadas

✅ .NET 8 

✅ MongoDB (banco NoSQL usado com MongoDB.Driver)

✅ AutoMapper (para conversão entre entidades e DTOs)

✅ xUnit (framework de testes unitários)

✅ Moq (mock de dependências para testes)

✅ Swagger / Swashbuckle (documentação da API)

✅ DDD (Domain-Driven Design) (estrutura do projeto)

✅ TDD (Test-Driven Development) (implementado em módulo específico)

✅ Logger (ILogger) (para logging de ações nos services)

✅ ASP.NET Core (estrutura MVC de controllers e middlewares)

✅ JWT (JSON Web Token) Autenticação de usuários e Autorização por perfil (ex: Administrador, Jogador)

---

## 📂 Estrutura do Projeto

```
fiap-cloud-games-api/
Application
├── Controllers
│   └── UsuarioController.cs, JogoController.cs
├── DTOs
│   ├── Requests
│   │   └── UsuarioCreateRequest.cs, etc.
│   └── Responses
│       └── UsuarioResponse.cs, etc.
├── Services
│   ├── UsuarioService.cs
│   └── Interfaces
│       └── IUsuarioService.cs
├── AutoMapper
│   └── MappingProfile.cs
├── Middlewares
│   └── ExceptionMiddleware.cs, etc.
Domain
├── Entities
│   └── Usuario.cs, Jogo.cs, etc.
├── Enums
│   └── PerfilUsuario.cs
├── Interfaces
│   └── IUsuarioRepository.cs, IJogoRepository.cs
├── ValueObjects (caso tenha tipos como CPF, Email, etc.)
Infrastructure
├── Context
│   └── MongoDbContext.cs (ou equivalente)
├── Repositories
│   ├── UsuarioRepository.cs, JogoRepository.cs
│   └── (implementações de IUsuarioRepository etc.)
├── Migrations (se necessário)
├── Configurations (separar mapeamentos ou settings)
Tests
├── Services
│   ├── UsuarioServiceTests.cs
│   └── JogoServiceTests.cs
```

---

## 🔧 Como Executar o Projeto

### 1. Clone o repositório

```bash
git clone https://github.com/rhaynnerliberato/fiap-cloud-games-api.git
cd fiap-cloud-games-api
```

### 2. Configure o `appsettings.json`

```json
{
  "Logging": {
        "LogLevel": {
            "Default": "Information",
            "Microsoft.AspNetCore": "Warning"
        }
    },
    "AllowedHosts": "*",
    "ConnectionStrings": {
        "MongoDb": "mongodb://localhost:27017"
    },
    "MongoDbSettings": {
        "ConnectionString": "mongodb://localhost:27017",
        "Database": "fiapcloudgamesdb"
    }
}
```

### 3. Execute a aplicação

```bash
dotnet run
```

A aplicação estará disponível em: `https://localhost:7242`

### 4. Acesse a documentação Swagger

```
https://localhost:7242/swagger/index.html
```

---

## 📄 Endpoints Disponíveis

### Usuários

* POST `/api/usuarios/cadastrar` - Cadastrar novo usuário
* GET `/api/usuarios/listar` - Listar todos os usuários
* GET `/api/usuarios/buscar-usuario/{id}` - Buscar por ID
* PUT `/api/usuarios/alterar-usuario/{id}` - Atualizar dados de um usuario
* DELETE `/api/usuarios/deletar-usuario/{id}` - Deletar usuário

### Autenticação
* POST `/api/Auth/login` - Login/autenticação
* GET `/api/Auth/erro` - Criado apenas para simular erro e testar o Middleware para Tratamento Global de Erros

### Jogos

* POST `/api/jogos/cadastrar` - Cadastrar novo jogo
* GET `/api/jogos/listar` - Listar todos os jogos
* GET `/api/jogos/buscar-jogo/{id}` - Buscar por ID
* PUT `/api/jogos/alterar-jogo/{id}` - Atualizar dados de um jogo
* DELETE `/api/jogos/deletar-jogo/{id}` - Deletar jogo

---

## 🔮 TDD Aplicado

Implementamos TDD no módulo de **Cadastrar Usuario**, com o ciclo clássico:

1. Criamos o teste primeiro (`UsuarioServiceTests.CadastrarAsync_EmailFiap_AssignsAdministradorPerfil...`).
2. Implementamos o método `CadastrarAsync` no `UsuarioService`.
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

## 👨‍💻 Desenvolvedor

** Rhaynner Liberato **
** Pos-tech em Arquitetura de Sistemas .Net - FIAP **
** Discord Username: rhaynner__ **
