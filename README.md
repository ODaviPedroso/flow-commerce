# FlowCommerce

FlowCommerce é uma aplicação de comércio eletrônico construída com .NET 10. O objetivo inicial é estabelecer uma base simples, testável e evolutiva para o desenvolvimento futuro da solução.

## Arquitetura

A solução utiliza separação de responsabilidades inspirada em Clean Architecture:

- `FlowCommerce.Domain`: núcleo de domínio, sem dependências de outros projetos;
- `FlowCommerce.Application`: casos de uso e orquestração da aplicação;
- `FlowCommerce.Infrastructure`: implementações e integrações técnicas;
- `FlowCommerce.Api`: entrada HTTP e composição da aplicação;
- `FlowCommerce.UnitTests`: testes unitários de Domain e Application;
- `FlowCommerce.IntegrationTests`: testes de integração da API.

Mais detalhes estão em [docs/architecture/overview.md](docs/architecture/overview.md).

## Estrutura da solução

```text
FlowCommerce/
├── docs/architecture/
├── src/
│   ├── FlowCommerce.Api/
│   ├── FlowCommerce.Application/
│   ├── FlowCommerce.Domain/
│   └── FlowCommerce.Infrastructure/
├── tests/
│   ├── FlowCommerce.UnitTests/
│   └── FlowCommerce.IntegrationTests/
└── FlowCommerce.slnx
```

## Requisitos

- .NET SDK 10

## Comandos

```powershell
dotnet restore
dotnet build
dotnet test
dotnet run --project src/FlowCommerce.Api
```

Com a API em execução, o health check está disponível em `GET /health` e retorna HTTP 200 com uma mensagem simples.

## Status

Em desenvolvimento.
