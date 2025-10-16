# API de Avisos - Teste Técnico Bernhoeft GRT

Sistema de gerenciamento de avisos desenvolvido em .NET 9.0 com arquitetura limpa, seguindo os padrões SOLID e implementando CQRS com MediatR.

## Objetivo

Implementar endpoints REST para operações CRUD completas no módulo de Avisos, incluindo campos de auditoria, validações robustas e soft delete para preservação de histórico.

## Funcionalidades Implementadas

### Endpoints REST

| Método | Endpoint | Descrição | Status de Resposta |
|--------|----------|-----------|-------------------|
| `GET` | `/api/v1/avisos` | Lista todos os avisos ativos | 200, 204 |
| `GET` | `/api/v1/avisos/{id}` | Busca aviso específico por ID | 200, 400, 404 |
| `POST` | `/api/v1/avisos` | Cria um novo aviso | 200, 400 |
| `PUT` | `/api/v1/avisos/{id}` | Atualiza mensagem do aviso | 204, 400, 404 |
| `DELETE` | `/api/v1/avisos/{id}` | Remove aviso (soft delete) | 204, 400, 404 |

### Recursos Principais

- **Campos de Auditoria**: DataCriacao e DataAlteracao para rastreamento completo
- **Validações**: FluentValidation aplicado em todos os endpoints
- **Soft Delete**: Preservação de dados históricos com flag Ativo
- **Filtro Automático**: Todas as consultas retornam apenas avisos ativos
- **Edição Restrita**: PUT edita apenas o campo Mensagem, conforme requisito
- **Documentação**: Swagger/OpenAPI completo e interativo

## Arquitetura

O projeto segue **Clean Architecture** dividida em 4 camadas:

```
┌─────────────────────────────────────┐
│   Presentation (API/Controllers)    │  Controllers, DTOs de entrada/saída
├─────────────────────────────────────┤
│   Application (Handlers)            │  Lógica de negócio, Validações
├─────────────────────────────────────┤
│   Domain (Entities/Interfaces)      │  Entidades de domínio, Regras de negócio
├─────────────────────────────────────┤
│   Infrastructure (Repositories)     │  Acesso a dados, Entity Framework Core
└─────────────────────────────────────┘
```

### Padrões de Design Utilizados

**CQRS (Command Query Responsibility Segregation)**
- Separação clara entre operações de leitura (Queries) e escrita (Commands)
- Queries: GetAvisoHandler, GetAvisosHandler
- Commands: CreateAvisoHandler, UpdateAvisoHandler, DeleteAvisoHandler
- Permite otimizações independentes e melhor organização do código

**Repository Pattern**
- Abstração da camada de acesso a dados
- Interface `IAvisoRepository` define o contrato
- `AvisoRepository` implementa a lógica de persistência
- Facilita testes e possível troca de tecnologia de banco de dados

**Dependency Injection**
- Utilização do container nativo do .NET
- Handlers recebem dependências via construtor através de `IServiceProvider`
- Facilita testes unitários com mocks

**FluentValidation**
- Validações declarativas e reutilizáveis
- Separação clara entre validação e lógica de negócio
- Fail-fast: validação ocorre antes de processar a requisição

## Pré-requisitos

- [.NET 9.0 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- IDE compatível: Visual Studio 2022, JetBrains Rider ou VS Code

## Como Executar

### 1. Clonar o repositório

```bash
git clone https://github.com/pmenezesdev/BasicoDotNet.git
cd BasicoDotNet
```

### 2. Restaurar dependências

```bash
dotnet restore
```

### 3. Compilar o projeto

```bash
dotnet build
```

### 4. Executar a aplicação

```bash
dotnet run --project 1-Presentation/Bernhoeft.GRT.Teste.Api/Bernhoeft.GRT.Teste.Api.csproj
```

### 5. Acessar a documentação Swagger

Abra o navegador em:
- HTTPS: `https://localhost:5001`
- HTTP: `http://localhost:5000`

## Testando os Endpoints

### Criar um novo aviso

```bash
curl -X POST "https://localhost:5001/api/v1/avisos" \
  -H "Content-Type: application/json" \
  -d '{
    "Titulo": "Manutenção Programada",
    "Mensagem": "Sistema indisponível das 22h às 23h para atualização"
  }'
```

### Listar todos os avisos ativos

```bash
curl -X GET "https://localhost:5001/api/v1/avisos"
```

### Buscar aviso por ID

```bash
curl -X GET "https://localhost:5001/api/v1/avisos/1"
```

### Atualizar mensagem de um aviso

```bash
curl -X PUT "https://localhost:5001/api/v1/avisos/1" \
  -H "Content-Type: application/json" \
  -d '{
    "Mensagem": "Sistema indisponível das 23h às 00h"
  }'
```

### Deletar um aviso (soft delete)

```bash
curl -X DELETE "https://localhost:5001/api/v1/avisos/1"
```

## Estrutura do Projeto

```
BasicoDotNet/
├── 0-Tests/
│   └── Bernhoeft.GRT.Teste.IntegrationTests/
├── 1-Presentation/
│   └── Bernhoeft.GRT.Teste.Api/
│       └── Controllers/v1/
│           └── AvisosController.cs
├── 2-Application/
│   └── Bernhoeft.GRT.Teste.Application/
│       ├── Handlers/
│       │   ├── Commands/v1/
│       │   │   ├── CreateAvisoHandler.cs
│       │   │   ├── UpdateAvisoHandler.cs
│       │   │   └── DeleteAvisoHandler.cs
│       │   └── Queries/v1/
│       │       ├── GetAvisoHandler.cs
│       │       └── GetAvisosHandler.cs
│       ├── Requests/
│       │   ├── Commands/v1/
│       │   │   └── Validations/
│       │   └── Queries/v1/
│       │       └── Validations/
│       └── Responses/
├── 3-Domain/
│   └── Bernhoeft.GRT.Teste.Domain/
│       ├── Entities/
│       │   └── AvisoEntity.cs
│       └── Interfaces/Repositories/
│           └── IAvisoRepository.cs
└── 4-Infra/
    └── Bernhoeft.GRT.Teste.Infra.Persistence.InMemory/
        ├── Mappings/
        │   └── AvisoMap.cs
        └── Repositories/
            └── AvisoRepository.cs
```

## Tecnologias Utilizadas

| Tecnologia | Versão | Finalidade |
|-----------|--------|------------|
| .NET | 9.0 | Framework principal |
| ASP.NET Core | 9.0 | Framework web |
| Entity Framework Core | 9.0.2 | ORM (InMemory Database para desenvolvimento) |
| MediatR | 12.4.1 | Implementação do padrão CQRS |
| FluentValidation | 11.11.0 | Validações de entrada de dados |
| Swashbuckle (Swagger) | 7.2.0 | Documentação OpenAPI/Swagger |
| Newtonsoft.Json | 13.0.3 | Serialização JSON |

## Decisões Técnicas e Justificativas

### 1. Campos de Auditoria

**Implementação:**
- `DataCriacao` (DateTime, obrigatório): Definido automaticamente no momento da criação
- `DataAlteracao` (DateTime?, nullable): Atualizado apenas em operações de UPDATE e DELETE

**Justificativa:**
- `DataCriacao` usa valor padrão `DateTime.UtcNow` para garantir timestamp consistente
- `DataAlteracao` é nullable para distinguir claramente registros que nunca foram modificados (null = nunca editado)
- Uso de UTC evita problemas com fusos horários e horário de verão
- Facilita auditoria e rastreamento de mudanças no sistema

### 2. Soft Delete

**Implementação:**
- Utiliza o campo `Ativo` (bool) já existente na entidade
- DELETE marca `Ativo = false` ao invés de remover o registro
- Atualiza `DataAlteracao` para registrar quando foi deletado

**Justificativa:**
- **Auditoria**: Preserva histórico completo de dados para conformidade e análises
- **Recuperação**: Permite desfazer exclusões acidentais sem necessidade de backup
- **Integridade Referencial**: Mantém relacionamentos de banco de dados intactos
- **Análise de Dados**: Dados deletados podem ser analisados futuramente
- **Trade-off**: Queries ficam ligeiramente mais complexas, mas os benefícios superam

### 3. Filtro Automático de Avisos Ativos

**Implementação:**
- Todas as queries incluem filtro `WHERE Ativo = true`
- Implementado na camada de repositório para centralização
- Avisos com `Ativo = false` são invisíveis para a API

**Justificativa:**
- Avisos inativos são considerados "deletados" do ponto de vista do usuário
- Evita vazamento acidental de dados deletados
- Simplifica a API (não precisa de flag nas requisições)
- Centraliza a lógica em um único lugar (Repository)

### 4. Validações com FluentValidation

**Implementação:**
- Validators dedicados para cada tipo de Request
- Validação automática antes de chegar ao Handler
- Mensagens de erro claras e descritivas

**Regras Implementadas:**
- **IDs**: Devem ser maiores que zero (previne IDs inválidos)
- **Título**: Obrigatório, máximo 50 caracteres
- **Mensagem**: Obrigatória, não pode ser vazia

**Justificativa:**
- **Separação de Responsabilidades**: Validação separada da lógica de negócio
- **Fail-Fast**: Requisições inválidas são rejeitadas antes de processamento
- **Performance**: Economiza recursos não processando dados inválidos
- **Testabilidade**: Validators podem ser testados isoladamente
- **Reutilização**: Validators podem ser compostos e reutilizados

### 5. Edição Restrita (Apenas Mensagem)

**Implementação:**
- Endpoint PUT aceita apenas o campo `Mensagem` no body
- Campo `Titulo` não pode ser alterado após criação

**Justificativa:**
- **Requisito do Teste**: Especificado explicitamente no teste técnico
- **Imutabilidade do Identificador**: Título funciona como identificador que não deve mudar
- **Rastreabilidade**: Evita confusão ao alterar identificadores
- **Simplicidade**: Responsabilidade clara do endpoint (Single Responsibility)

### 6. TrackingBehavior do Entity Framework

**Implementação:**
- **NoTracking** usado em operações de leitura (Queries)
- **Tracking padrão** usado em operações de escrita (Commands)

**Justificativa:**
- **Performance em Queries**: NoTracking é 30-50% mais rápido em leituras
- **Economia de Memória**: Não mantém entidades no Change Tracker
- **Necessário em Updates**: Tracking permite que EF detecte mudanças automaticamente
- **Change Detection**: Com tracking, apenas campos modificados são atualizados no banco

**Exemplo:**
```csharp
// Query (NoTracking)
var aviso = await _avisoRepository.ObterAvisoPorIdAsync(id, TrackingBehavior.NoTracking);

// Command (Tracking)
var aviso = await _avisoRepository.ObterAvisoPorIdAsync(id, TrackingBehavior.Default);
aviso.Mensagem = "Nova mensagem";  // EF detecta a mudança
await _context.SaveChangesAsync();  // Gera UPDATE apenas do campo Mensagem
```

### 7. Uso de DateTime.UtcNow

**Implementação:**
- Todos os timestamps utilizam `DateTime.UtcNow` ao invés de `DateTime.Now`

**Justificativa:**
- **Consistência Global**: UTC não muda com horário de verão ou fusos horários
- **Comparações Confiáveis**: Timestamps são sempre comparáveis diretamente
- **Interoperabilidade**: Facilita integração com sistemas em diferentes regiões
- **Boas Práticas**: Padrão da indústria para aplicações distribuídas

### 8. Operadores de Conversão Implícita

**Implementação:**
```csharp
public static implicit operator GetAvisoResponse(AvisoEntity entity) => new()
{
    Id = entity.Id,
    Titulo = entity.Titulo,
    // ...
};
```

**Justificativa:**
- **Código Limpo**: Mapeamento simples sem bibliotecas externas (AutoMapper)
- **Type-Safe**: Erros detectados em tempo de compilação
- **Performance**: Zero overhead de reflexão
- **Consistência**: Mantém padrão já existente no projeto

### 9. Async/Await em Operações de I/O

**Implementação:**
- Todos os métodos que acessam banco de dados são assíncronos
- CancellationToken sempre propagado para permitir cancelamento

**Justificativa:**
- **Escalabilidade**: Permite alta concorrência sem bloquear threads
- **Responsividade**: Aplicação continua responsiva durante operações longas
- **Boas Práticas**: Padrão recomendado para operações de I/O no .NET

### 10. Dependency Injection com IServiceProvider

**Implementação:**
```csharp
private readonly IServiceProvider _serviceProvider;
private IContext _context => _serviceProvider.GetRequiredService<IContext>();
```

**Justificativa:**
- **Lazy Loading**: Dependências só são resolvidas quando necessário
- **Padrão do Projeto**: Mantém consistência com código existente
- **Flexibilidade**: Fácil adicionar novas dependências
- **Testabilidade**: Simples mockar IServiceProvider em testes unitários

## Validações Implementadas

### GET /avisos/{id}
- ID deve ser maior que zero

### POST /avisos
- Título não pode ser vazio
- Título não pode exceder 50 caracteres
- Mensagem não pode ser vazia

### PUT /avisos/{id}
- ID deve ser maior que zero
- Mensagem não pode ser vazia

### DELETE /avisos/{id}
- ID deve ser maior que zero

## Exemplos de Resposta

### Sucesso (200 OK)

```json
{
  "Id": 1,
  "Ativo": true,
  "Titulo": "Manutenção Programada",
  "Mensagem": "Sistema indisponível das 22h às 23h",
  "DataCriacao": "2025-10-13T19:30:00Z",
  "DataAlteracao": null
}
```

### Lista de Avisos (200 OK)

```json
[
  {
    "Id": 1,
    "Ativo": true,
    "Titulo": "Titulo 1",
    "DataCriacao": "2025-10-13T10:30:00Z",
    "DataAlteracao": null
  },
  {
    "Id": 2,
    "Ativo": true,
    "Titulo": "Titulo 2",
    "DataCriacao": "2025-10-13T11:45:00Z",
    "DataAlteracao": "2025-10-13T15:20:00Z"
  }
]
```

### Erro de Validação (400 Bad Request)

```json
{
  "errors": {
    "Id": ["O ID deve ser maior que zero."],
    "Mensagem": ["A mensagem não pode ser vazia."]
  }
}
```

### Recurso Não Encontrado (404 Not Found)

```json
{
  "message": "Not Found"
}
```

### Sem Conteúdo (204 No Content)

Retornado em operações bem-sucedidas de PUT e DELETE, ou quando GET não retorna resultados.

## Melhorias Futuras

Caso este projeto evoluísse para produção, as seguintes melhorias seriam prioritárias:

**Testes Automatizados**
- Testes unitários para todos os Handlers
- Testes de integração para endpoints completos
- Testes de validação para FluentValidators
- Testes de carga para avaliar performance

**Paginação e Filtros**
- Implementar paginação no endpoint GET /avisos
- Adicionar filtros por data, título, status
- Permitir ordenação customizável

---

Desenvolvido para o Teste Técnico Bernhoeft GRT
