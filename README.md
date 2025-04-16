# Health&Med

## Requisitos
### Funcionais
- Cadastro de médicos
- Autenticação de médicos
- Cadastro/Edição de horários dísponiveis de médicos
- Cadastro de pacientes
- Aunteticação de pacientes
- Busca por médicos
- Agendamento de consultas
- Cancelamento de consultas
- Confirmação de consultas

### Não funcionais
- Concorrência de agendamento: O sistema deve garantir que o agendamento seja permitida para um determinado horários.
- Validação de conflito de horários: O sistema deve validar a disponibilidade do horário selecionado e assegurar que não haja sobreposição de horários para consultas agendadas.

<img src="./assets/arch.png">

##  Tarefas
- [x] Script SQL
- [x] Documentação
- [x] Estrutura
    - [x] Classes compartilhadas
    - [x] Value Objects
        - [x] Nome
        - [x] Senha
        - [x] Cpf
        - [x] Email
        - [x] Crm
    - [x] Entidades
    - [x] Conexão com banco de dados
- [x] Endpoints
    - [x] POST | Autenticação médico
    - [x] POST | Criação de médico
    - [x] POST | Criação de horário disponível de médico
    - [x] PUT | Atualização de horário de médico
    - [x] GET | Listagem de medicos
    - [x] GET | Listagem de horarios disponiveis por médico
    - [x] POST | Agendamento de paciente e médico
    - [x] PATCH | Cancelamento de agendamento
    - [x] PATCH | Aceitação/Recusa de agendamento
    - [x] POST | Criação paciente
    - [x] POST | Autenticação paciente
- [x] Testes unitários
    - [x] Value Objects
    - [x] Pacientes
    - [x] Doutores
- [x] CI/CD
- [x] Autenticação
    - [x] Policy para Doutores
    - [x] Policy para Pacientes
    
## Métodos

Login de medicos

```http
POST /api/medicos/login
```
Cadastro de médico

```http
POST /api/medicos
```
Cadastro de horarios por médico

```http
POST /api/medicos/horarios
```
Atualização de horarios de médico

```http
PUT /api/medicos/{horarioId}/agendar
```
Listar medicos
```http
GET /api/medicos/buscar?page=1
```
Listar horarios disponiveis de medico

```http
GET /api/medicos/{medicoId}/agenda-disponivel
```
Agendamento de consulta

```http
POST /api/medicos/{horarioId}/{pacienteId}/agendamentos
```
Cancelamento de agendamento

```http
PATCH /api/medicos/horarios/{horarioId}/cancelar-agendamento
```
Aceitação/Recusa de agendamento

```http
PATCH /api/medicos/horarios/{horarioId}/status/{status}
```
Cadastro de pacientes

```http
POST /api/pacientes
```
Login de pacientes

```http
POST /api/pacientes/login
```



