
LINK GITHUB:  https://github.com/M4rkitos/SPT4-.NET

LINK PITCH: https://youtu.be/vwAgvu-1zRQ

Integrantes
Nomes: Marcos Vinicius | Jonas Ikimio | Daniel Kendi
RMs: 560475 | 560560 | 553043



## 🚀 Funcionalidades Principais (Sprint 4)

- **CRUD Completo de Vagas:** Criação, consulta, atualização e eliminação de reservas de vagas (`VagaReserva`).
- **Algoritmo de Paginação e Filtros:** Consultas otimizadas no banco de dados utilizando os métodos `.Skip()` e `.Take()` do Entity Framework Core.
- **Navegabilidade Dinâmica (HATEOAS):** Implementação de Maturidade REST Nível 3, injetando links dinâmicos (`self`, `nextPage`, `prevPage`) nas respostas da API.
- **Tratamento Global de Exceções:** Middleware customizado que captura falhas em qualquer camada da aplicação e centraliza o retorno em respostas JSON padronizadas com HTTP Status adequados.
- **Monitorização e Observabilidade:** Configuração nativa de *Health Checks* expostos no endpoint `/health`, integração com Serilog para logs estruturados e OpenTelemetry para rastreio.

---

## 🏗️ Arquitetura da Solução

O projeto está dividido em 4 camadas bem estruturadas para respeitar a inversão de dependência:

1. **EasyAccess.Domain:** O coração da aplicação. Contém as entidades de negócio (`VagaReserva`) e os contratos/interfaces dos repositórios (`IVagaRepository`). Não tem dependências externas.
2. **EasyAccess.Application:** Camada responsável pelas regras de negócio e casos de uso. Contém os Serviços (`VagaService`), as interfaces de serviço (`IVagaService`) e os objetos de transferência de dados (`DTOs`).
3. **EasyAccess.Infrastructure:** Implementação do acesso a dados e persistência. Contém o contexto do banco de dados (`EasyAccessDbContext`) e a implementação concreta dos repositórios (`VagaRepository`) utilizando o Entity Framework Core.
4. **EasyAccess.Api:** A porta de entrada da aplicação. Contém os `Controllers` REST, os ficheiros de configuração (`Program.cs`) e os `Middlewares` globais.

---

## 🛠️ Tecnologias Utilizadas

- **Plataforma:** .NET 8 / C#
- **ORM:** Entity Framework Core
- **Log Estruturado:** Serilog
- **Telemetria:** OpenTelemetry (Console Exporter)
- **Documentação:** OpenAPI / Swagger
- **Testes de Integração:** Postman

---

## 🚀 Como Executar o Projeto

### Pré-requisitos
- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0) instalado.
- IDE de sua preferência (VS Code ou Visual Studio).

### Passos para Execução
1. Clone este repositório para a sua máquina local.
2. Abra o terminal na pasta raiz do projeto (onde se encontra o ficheiro `EasyAccess.sln`).
3. Execute o comando para restaurar as dependências:
   ```bash
   dotnet restore




   Execute a aplicação a partir da camada de API:

Bash
dotnet run --project EasyAccess.Api
Aceda à documentação interativa do Swagger diretamente no seu navegador através do endereço local indicado no terminal (ex: http://localhost:5000/index.html ou correspondente HTTPS).

📇 Endpoints Principais para Teste
Monitorização
GET /health - Retorna o estado atual de saúde da aplicação (Healthy).

Reservas de Vagas
GET /api/Vagas - Lista paginada de vagas com suporte aos parâmetros query: page, pageSize, placa e ordenacao.

GET /api/Vagas/{id} - Recupera os detalhes de uma reserva específica e retorna os links HATEOAS correspondentes.

POST /api/Vagas - Realiza uma nova reserva de vaga (Valida se a data de início é anterior à data de fim).

PUT /api/Vagas/{id} - Atualiza os dados de uma reserva existente.

DELETE /api/Vagas/{id} - Remove uma reserva do sistema.