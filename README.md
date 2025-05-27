
# BeeBilling - Desafio Omnibees

## Descrição

Este projeto é um desafio técnico proposto pela Omnibees para a área de BeeBilling.  
Trata-se de uma aplicação REST API desenvolvida em **.NET Core 8**, utilizando **Entity Framework Core** para acesso a dados com **SQL Server** rodando em container Docker.  

A arquitetura do sistema segue os princípios do **Domain-Driven Design (DDD)**, os padrões do **SOLID** e a arquitetura limpa (**Clean Architecture**), garantindo alta manutenibilidade, escalabilidade e qualidade no código.  

A API é totalmente **assíncrona**, proporcionando melhor desempenho e escalabilidade.

---

## Tecnologias Utilizadas

- .NET Core 8
- SQL Server 2022 (Docker)
- Entity Framework Core (EF Core)
- REST API Async
- SOLID Principles
- Domain-Driven Design (DDD)
- Clean Architecture
- Docker Compose

---

## Como subir o projeto com Docker

O projeto possui um `docker-compose.yaml` para facilitar o setup do ambiente, contendo os seguintes serviços:

- **sqlserver**: Container com SQL Server 2022 configurado.
- **api**: Container com a API REST compilada e rodando no ASP.NET Core 8.

### Passos para rodar

1. Certifique-se de ter o **Docker** instalado e rodando em sua máquina.

2. Clone este repositório:
   ```bash
   git clone git@github.com:xgandrade/Omnibees.BeeBilling.git
   cd Omnibees.BeeBilling
   ```

3. Execute o comando para subir os containers:
   ```bash
   docker-compose up --build
   ```

4. O SQL Server estará disponível na porta **1440** (usuário `sa`, senha configurada no `docker-compose.yaml`).

5. A API estará disponível no endereço:
   ```
   http://localhost:5000
   ```

---

## Estrutura do Docker Compose

```yaml
version: "3.9"

services:
  sqlserver:
    image: mcr.microsoft.com/mssql/server:2022-latest
    container_name: beebilling_sqlserver
    environment:
      SA_PASSWORD: "Omnibees!Loc@l2025"
      ACCEPT_EULA: "Y"
      MSSQL_PID: "Developer"
    ports:
      - "1440:1433"
    volumes:
      - sql_data:/var/opt/mssql
    networks:
      - beebilling-network

  api:
    build:
      context: .
      dockerfile: Omnibees.BeeBilling.Api/Dockerfile
    ports:
      - "5000:80"
    networks:
      - beebilling-network

volumes:
  sql_data:

networks:
  beebilling-network:
```

---

## Dockerfile da API

```dockerfile
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["./Omnibees.BeeBilling.Api/Omnibees.BeeBilling.Api.csproj", "."]
COPY ["../Omnibees.BeeBilling.Application/Omnibees.BeeBilling.Application.csproj", "../Omnibees.BeeBilling.Application/"]
COPY ["../Omnibees.BeeBilling.Domain/Omnibees.BeeBilling.Domain.csproj", "../Omnibees.BeeBilling.Domain/"]
COPY ["../Omnibees.BeeBilling.Infrastructure/Omnibees.BeeBilling.Infrastructure.csproj", "../Omnibees.BeeBilling.Infrastructure/"]

RUN dotnet restore "Omnibees.BeeBilling.Api.csproj"

COPY ../ .

WORKDIR /src/Omnibees.BeeBilling.Api
RUN dotnet publish "Omnibees.BeeBilling.Api.csproj" -c Debug -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
EXPOSE 80
ENTRYPOINT ["dotnet", "Omnibees.BeeBilling.Api.dll"]
```

---

## Contato

Guilherme Silva de Andrade
