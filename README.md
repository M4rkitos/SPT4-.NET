Projeto EasyAccess (Versão SPRINT 2)
# EasyAccess: Gestão Condominial Inteligente
O EasyAccess é uma plataforma que implementa Compliance e Qualidade (QA) na gestão de condomínios. O projeto utiliza Clean Architecture em .NET Core 8.0 para prover uma solução transparente e justa para a gestão de recursos compartilhados.

1. Definição e Evolução do Projeto (Visão Geral Atualizada)
1.1. Objetivo Principal
O EasyAccess visa resolver os desafios de gestão de vagas de visitantes e reservas de áreas comuns em condomínios residenciais. Ele automatiza processos, garante equidade através de um sistema de sorteio justo e otimiza a receita do condomínio.

1.2. Progresso e Novas Funcionalidades (SPRINT 2) 🚀
O projeto evoluiu para um sistema funcional com foco total na Camada Web (API) e Deployment Conteinerizado. As novas funcionalidades implementadas incluem:

CRUD Completo (API): Implementação dos métodos Create, Read, Update, e Delete para o domínio de Reservas.

Busca Avançada (Search): Rota GET /api/vagas/search com suporte a paginação, ordenação e filtros.

HATEOAS: Aplicação do conceito de Hypermedia (links de ação) na resposta da API (GET by ID).

Arquitetura DevOps: Configuração completa para deployment conteinerizado (Docker, Docker Compose) na Nuvem Linux.

1.3. Escopo Atual da API
Abaixo estão os módulos implementados:

Gestão de Vagas: CRUD completo de Reservas de Vagas com HATEOAS.

Regras de Negócio: Sistema de Cobrança Progressiva e Lógica de Sorteio Pseudo Aleatório.

Tecnologias: .NET 8.0, Clean Architecture, Entity Framework Core, Docker, Docker Compose, Nuvem IaaS.

2. Requisitos Funcionais e Não Funcionais
2.1. Funcionais
Permitir que moradores consultem a disponibilidade de vagas e áreas comuns em tempo real.

Permitir a reserva de vagas e áreas comuns com confirmação instantânea.

Implementar as quatro operações CRUD (Create, Read, Update, Delete).

Suportar rotas de busca com filtros e paginação.

Executar o algoritmo de sorteio para datas especiais.

Calcular e exibir o custo das vagas de visitantes com base no tempo de uso.

2.2. Não Funcionais (Manutenção)
Disponibilidade: A plataforma deve estar disponível 99,9% do tempo.

Escalabilidade: A arquitetura deve suportar um crescimento de até 1000 condomínios e 50.000 usuários sem degradação de performance.

Segurança: Utilizar autenticação segura (ex: JWT) e criptografia de dados em trânsito e em repouso.

3. Estrutura do Repositório e Instruções de Deployment
3.1. Arquivos Essenciais
O repositório está estruturado em camadas e inclui os arquivos necessários para o deployment conteinerizado:

Pastas da Clean Architecture: EasyAccess.Api/, EasyAccess.Application/, EasyAccess.Domain/, EasyAccess.Infrastructure/.

Dockerfile: Receita multi-stage para construção da imagem da API (easyaccess-api).

docker-compose.yml: Orquestra e inicia a API e o Banco de Dados simulado.

3.2. Configuração de Deployment
Pré-requisitos: Docker Engine, Docker Compose instalados e Cliente SSH configurado.

Variáveis: Garanta que a senha do Banco de Dados no docker-compose.yml (variável SA_PASSWORD) esteja configurada.

3.3. Execução do Deployment (Modo Background)
Para iniciar os serviços, utilize o Docker Compose:

Clonar e Navegar:

Bash

git clone <URL_DO_SEU_REPO>
cd <PASTA_RAIZ_DO_PROJETO>
Build e Run:

O comando a seguir constrói a imagem .NET a partir do Dockerfile e inicia a API e o BD simulado em segundo plano (-d).

Bash

docker compose up -d --build
Verificar Status:

Bash

docker ps
Resultado esperado: Os containers api-1 e db-1 devem estar no status Up.

4. Testes Funcionais da API
A API estará disponível na porta 8080. Utilize seu cliente HTTP para validar as funcionalidades.

URL Base: Use o IP da sua VM na Nuvem ou localhost (se rodando localmente): http://<IP_PÚBLICO_DA_VM>:8080

4.1. CREATE (Post) - Teste de Persistência
Método: POST

Rota: /api/vagas

JSON de Exemplo: {"moradorId": 1, "dataInicio": "2025-11-10T10:00:00", "dataFim": "2025-11-10T14:00:00", "placaVeiculo": "ABC-1234"}

4.2. READ (Get) - Busca Avançada
Método: GET

Rota: /api/vagas/search?PageNumber=1&PageSize=5&SortBy=DataInicio

4.3. READ (Get) - HATEOAS
Método: GET

Rota: /api/vagas/1 (Assumindo que o ID 1 existe).

Resultado Esperado: O JSON deve incluir o objeto Reserva e um array de Links para as ações PUT e DELETE.

4.4. DELETE
Método: DELETE

Rota: /api/vagas/1

Resultado Esperado: Código de Status 204 No Content (Confirma a exclusão).


# 🚀 EasyAccess - Challenge Sprint 3

## 🛠️ Tecnologias e Implementações (Sprint 3)
Esta sprint focou em **Observabilidade, Testes Automatizados e DevOps**.

### 1. Monitoramento e Observabilidade (40 pts)
- **Health Checks:** Endpoint `/health` configurado para validar a saúde da API e do banco de dados.
- **Logging Estruturado:** Implementação do **Serilog** gravando logs em console e arquivos rotativos na pasta `/logs`.
- **Tracing e Métricas:** Integração com **OpenTelemetry** para rastreio de performance e tempo de resposta das requisições.

### 2. Testes Automatizados (50 pts)
- **Framework:** xUnit.
- **Padrão AAA:** Todos os testes seguem a estrutura *Arrange, Act, Assert*.
- **Mocks:** Uso da biblioteca **Moq** para isolar as dependências da camada de aplicação.
- **Como executar:** No terminal, navegue até a pasta de testes e digite:
  ```bash
  dotnet test

### 3. DevOps e Containerização
Docker: Arquivo Dockerfile multi-stage otimizado para produção.

Docker Compose: Configuração pronta para subir a API e o SQL Server de forma orquestrada.

Como rodar (necessário Docker instalado):
docker-compose up --build

### 4.Estrutura do Projeto
EasyAccess.Api: Ponto de entrada e configurações de monitoramento.

EasyAccess.Application: Regras de negócio e serviços.

EasyAccess.Domain: Entidades e interfaces.

EasyAccess.Infrastructure: Persistência de dados e repositórios.

EasyAccess.Tests: Projeto de testes automatizados.