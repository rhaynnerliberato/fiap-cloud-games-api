# Fiap Cloud Games - Documentação do Projeto

## 🌟 Objetivo do Projeto

Este é um sistema de backend RESTful voltado para uma plataforma de gerenciamento de jogos na nuvem, chamada **Fiap Cloud Games**. A API permite operações de CRUD para usuários e jogos, autenticação de usuário e funcionalidades adicionais como promoção de jogos.

Foi desenvolvida em .NET 8 com MongoDB, utilizando boas práticas como injeção de dependência, TDD e arquitetura DDD (Controller, Service, Repository, Domain).

---

## 🚀 Tecnologias Utilizadas

* .NET 8
* MongoDB
* AutoMapper
* xUnit
* Moq
* Swagger (Swashbuckle)

---

## 📂 Estrutura do Projeto

```
fiap-cloud-games-api/
|-- Controllers/
|-- Domain/
|   |-- Entities/
|   |-- Enums/
|   |-- Interfaces/
|-- Models/
|   |-- Requests/
|   |-- Responses/
|-- Repositories/
|-- Services/
|-- Tests/
|   |-- Services/
|-- Program.cs
|-- appsettings.json
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
* GET `/api/Auth/erro` - Simular erro - middleware para tratamento global de erros

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
