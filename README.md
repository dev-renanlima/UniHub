# UniHub

**UniHub** é uma plataforma de gestão acadêmica voltada para professores e alunos, que facilita a organização de disciplinas, atividades e a comunicação em ambiente virtual.

📌 **Diagrama (Draw.io):**  
[Visualizar Diagrama](https://drive.google.com/file/d/1p-CYF8Kh5j42eQb1ObHBkWp4ufrlQ0-s/view?usp=sharing)

---

## 👥 Tipos de Usuário

### 👨‍🏫 Professor
- Criar disciplinas
- Convidar alunos via e-mail ou link (código da disciplina)
- Criar atividades e trabalhos  
  - Notificação via **UniCalendar**
- Publicar notícias e avisos  
  - Notificação via **UniCalendar**
- Fazer upload de materiais

### 👩‍🎓 Aluno
- Fazer download de materiais
- Visualizar tarefas no **UniCalendar**
- Fazer upload de tarefas
- Acompanhar status das tarefas via **Kanban**

---

## 💬 Chat

- Criado automaticamente por disciplina
- Todos os usuários têm acesso e podem enviar mensagens

---

## 📝 Criar Conta

### Professor
1. Criar conta no **Clerk**
2. Inserir usuário como **Admin**
3. Criar disciplina  
   - Inserir nova disciplina (**Admin**)  
   - Associar professor à disciplina  
   - Gerar código da disciplina (ex: `2507291234` - `Ano{2}Mês{2}Dia{2}Números aleatórios{4}`)

### Aluno
1. Criar conta no **Clerk**
2. Inserir usuário como **Member**
3. Inserir código da disciplina
   - Buscar disciplina
   - Associar aluno como **Member**

---

## ⬆️⬇️ Upload / Download de Arquivos

- Arquivos são salvos no **Supabase**
- URLs de acesso são geradas e compartilhadas com os usuários

---

## 💬 Chat - Funcionalidade

- Usuário envia mensagem  
  → Mensagem é salva no banco de dados

---

## 🛠️ Tecnologias Utilizadas

- **.NET 8+**
- **PostgreSQL 16+**
- **Clean Architecture**
- **FluentValidation** – Validação de dados
- **Mapster** – Mapeamento de objetos (DTO ↔ Entidades)
- **Injeção de Dependência (Built-in .NET)**
- **Unit of Work** – Gerenciamento de transações e repositórios
- **Supabase** – Armazenamento de arquivos
- **Clerk** – Autenticação de usuários

---

## 📁 Estrutura do Projeto

A arquitetura segue os princípios da **Clean Architecture**, dividindo responsabilidades em camadas:

```plaintext
/src
│
├── Application       # Casos de uso e validações (FluentValidation)
├── Domain            # Entidades e interfaces
├── Infrastructure    # Implementações concretas (repos, db, auth)
├── Persistence       # Contexto do banco e UnitOfWork (PostgreSQL)
├── WebAPI            # Endpoints (Controllers), Middlewares, DI
```

---
## 🚀 Como Rodar o Projeto Localmente

### Pré-requisitos

- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- [PostgreSQL 16+](https://www.postgresql.org/)
- [Supabase Account](https://supabase.com/)
- [Clerk Account](https://clerk.dev/)

---

### Passos

1. **Clone o repositório:**

   ```bash
   git clone https://github.com/seu-usuario/unihub.git
   cd unihub
   ```
   
2. **Configure o banco de dados:**

- Crie um banco PostgreSQL
- Atualize a ConnectionString no arquivo appsettings.json
- Configure Supabase e Clerk:
- Adicione as chaves de API e URLs nos arquivos de configuração da aplicação

3. **Restaure os pacotes e compile o projeto:**

```bash
dotnet restore
dotnet build
```

4. **Execute o projeto:**
```bash
dotnet run --project src/WebAPI
```

---

## 📌 Observações

- O UniCalendar centraliza todas as notificações relacionadas a tarefas e avisos.
- O Chat é gerado automaticamente por disciplina, com suporte a mensagens em tempo real (dependendo da implementação) e persistência no banco de dados.

---

## 📄 Licença
- Este projeto está licenciado sob os termos da [MIT License](https://x).
