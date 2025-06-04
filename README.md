# SOS ClimaTech - Global Solution FIAP

## Descrição

Este projeto é uma aplicação web desenvolvida em ASP.NET Core 8 para a Global Solution da FIAP. O objetivo é criar um sistema para cadastrar eventos climáticos extremos e as pessoas afetadas por eles, auxiliando na gestão de informações durante crises.

A aplicação possui uma interface web (Razor Pages) para interação e uma API REST (documentada com Swagger) para possíveis integrações futuras.

## Funcionalidades

*   Eventos Climáticos: CRUD completo (Criar, Ler, Atualizar, Deletar).
*   Pessoas Afetadas: CRUD completo, com associação obrigatória a um evento climático (relacionamento 1:N).
*   API REST: Endpoints para todas as operações CRUD de ambas as entidades.
*   Documentação API: Swagger disponível em ambiente de desenvolvimento (`/swagger`).
*   Persistência: Banco de dados SQLite gerenciado via Entity Framework Core e Migrations.

## Tecnologias

*   .NET 8 / ASP.NET Core 8
*   Entity Framework Core 8
*   SQLite
*   Razor Pages
*   Swagger (Swashbuckle)
*   Bootstrap

*   **Apresentação:** Razor Pages e Controllers API.
*   **Lógica de Negócios:** Services.
*   **Acesso a Dados:** Repositories com EF Core.
*   **Banco de Dados:** SQLite.

## Como Executar

**Pré-requisitos:**

*   SDK do .NET 8.

**Passos:**

1.  Clone ou baixe o repositório.
2.  Navegue até a pasta `GsDotNet/src` via terminal.
3.  Execute `dotnet run`.
4.  Acesse a aplicação pela URL informada no terminal (ex: `http://localhost:5000`).

**Para ver o Swagger:**

1.  Pare a aplicação (`Ctrl+C`).
2.  Defina a variável de ambiente: `ASPNETCORE_ENVIRONMENT=Development` (o comando exato varia entre cmd, PowerShell e bash).
3.  Execute `dotnet run` novamente.
4.  Acesse `/swagger` na URL base (ex: `http://localhost:5000/swagger`).

## Testes

**Interface Web:**

*   Acesse a aplicação e navegue pelos menus "Eventos" e "Pessoas Afetadas".
*   Realize operações de criação, visualização, edição e exclusão para ambas as entidades.
*   Verifique o funcionamento das validações (ex: tentar salvar pessoa sem evento).


## Desenvolvimento

O projeto foi desenvolvido utilizando as tecnologias e padrões mencionados, com foco em atender aos requisitos da Global Solution. A estrutura busca organizar o código de forma clara, separando responsabilidades entre controllers, services e repositories. O Entity Framework Core facilita a interação com o banco de dados SQLite, e as Migrations controlam o versionamento do schema.
