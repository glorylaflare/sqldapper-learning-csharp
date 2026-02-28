# SQL Dapper Learning (C#)

API simples de tarefas (Todo) criada para estudar e praticar **Dapper** com **.NET 8** usando **Minimal API**.

## Objetivo

Este projeto foi construído com foco em simplicidade para treino de:

- modelagem básica de API REST
- acesso a dados com Dapper
- uso de SQL parametrizado
- organização mínima em camadas (Endpoints, Repository, DTOs, Models)

## Stack

- .NET 8
- ASP.NET Core Minimal API
- Dapper
- SQL Server / LocalDB
- Swagger (OpenAPI)

## Estrutura

- `todo-api.slnx` - solução
- `Todo.Api/Program.cs` - configuração da aplicação e DI
- `Todo.Api/Extensions/MinimalApi.cs` - endpoints
- `Todo.Api/Repository/TodoRepository.cs` - queries com Dapper
- `Todo.Api/Interfaces/ITodoRepository.cs` - contrato do repositório
- `Todo.Api/Dto/TodoDto.cs` - DTO de entrada
- `Todo.Api/Models/` - entidades de domínio

## Pré-requisitos

- .NET SDK 8 instalado
- SQL Server LocalDB (ou SQL Server compatível)

## Configuração

### 1) Connection string

Por padrão, o projeto usa a connection string no arquivo:

- `Todo.Api/appsettings.json`

Valor atual:

```json
"ConnectionStrings": {
	"DefaultConnection": "Server=(localdb)\\MSSQLLocalDB;Database=LeaningPracticeTodoDb;Trusted_Connection=True;TrustServerCertificate=True;"
}
```

Se necessário, ajuste para sua instância local.

### 2) Criar tabela no banco

Execute o script abaixo no banco configurado:

```sql
CREATE TABLE Todos (
		Id INT IDENTITY(1,1) PRIMARY KEY,
		Title NVARCHAR(200) NOT NULL,
		Description NVARCHAR(1000) NOT NULL,
		IsDone BIT NOT NULL,
		CreatedAt DATETIME2 NOT NULL DEFAULT SYSUTCDATETIME(),
		UpdatedAt DATETIME2 NULL
);
```

## Como executar

Na raiz do repositório:

```powershell
dotnet restore .\todo-api.slnx
dotnet build .\todo-api.slnx
dotnet run --project .\Todo.Api\Todo.Api.csproj
```

Swagger:

- http://localhost:5041/swagger
- https://localhost:7005/swagger

## CI/CD (GitHub Actions)

O projeto possui dois workflows em `.github/workflows`:

- `ci.yml`:
	- dispara em `push` (`main`, `develop`) e `pull_request` para `main`
	- executa `restore`, `build`, `test` e `publish`
	- publica artefato `todo-api-publish`

- `cd.yml`:
	- dispara em `push` para `main`, tags `v*` e manual (`workflow_dispatch`)
	- faz build e push da imagem Docker para o GHCR
	- imagem base: `ghcr.io/<owner>/todo-api`

### Requisitos para CD

- Repositório hospedado no GitHub
- GitHub Actions habilitado
- Permissão de packages (já definida no workflow com `packages: write`)

Após o primeiro push para `main`, a imagem deve aparecer em:

- `ghcr.io/<seu-usuario-ou-org>/todo-api`

## Endpoints

Base route: `/api/todos`

- `POST /` - cria um todo
- `GET /` - lista todos
- `GET /{id}` - busca por id
- `GET /notdone` - lista por status via query `isDone` (ex.: `GET /notdone?isDone=false` lista tarefas pendentes)
- `PUT /{id}` - atualiza
- `DELETE /{id}` - remove

Payload de exemplo (`TodoDto`):

```json
{
	"title": "Estudar Dapper",
	"description": "Praticar queries e mapeamento",
	"isDone": false
}
```

## Observações

- Projeto intencionalmente simples para aprendizado.
- Para evolução futura, veja o review em `Todo.Api/Docs/code-review.md`.

## Licença

Uso livre para estudo e prática.

