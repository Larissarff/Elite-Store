# 🛒 AppEcommerce — Clean Architecture (DDD)

Este projeto é um **e-commerce** desenvolvido em **C# com ASP.NET Core**, estruturado segundo os princípios de **Domain-Driven Design (DDD)** e **Clean Architecture**.

O objetivo é criar uma aplicação modular, escalável e de fácil manutenção, dividida em camadas que isolam regras de negócio, lógica de aplicação, infraestrutura e interface web (API).

---

## 🧱 Estrutura da Solução

```
AppEcommerce/

 └── src/

     ├── AppEcommerce.API          → Camada de apresentação (Controllers e Endpoints)

     ├── AppEcommerce.Application  → Casos de uso, DTOs e serviços de aplicação

     ├── AppEcommerce.Domain       → Entidades, Value Objects e interfaces do domínio

     ├── AppEcommerce.Infra.Data   → Persistência, DbContext e repositórios

     └── AppEcommerce.sln          → Solution principal
```

---

## 🧩 Camadas e Responsabilidades

| Camada | Descrição |
|--------|------------|
| **Domain** | Contém as **entidades centrais** e **regras de negócio** puras. Não depende de nenhuma outra camada. |
| **Application** | Orquestra os **casos de uso** e faz a comunicação entre a API e o domínio. Utiliza DTOs e services. |
| **Infra.Data** | Implementa os repositórios, o contexto do banco de dados (Entity Framework Core) e configurações de persistência. |
| **API** | Camada mais externa, responsável por expor endpoints HTTP e receber requisições. |

---

## ⚙️ Tecnologias Utilizadas

- **.NET 8.0**
- **ASP.NET Core Web API**
- **Entity Framework Core**
- **Dependency Injection (DI)**
- **Swagger / Swashbuckle**
- **SQL Server (ou SQLite para desenvolvimento)**

---

## 🚀 Como Executar o Projeto

### 1️⃣ Clonar o repositório
```bash
git clone https://github.com/Larissarff/AppEcommerce.git
cd AppEcommerce/src/AppEcommerce.API
```

### 2️⃣ Restaurar dependências
```bash
dotnet restore
```

### 3️⃣ Rodar a aplicação
```bash
dotnet run
```

O servidor será iniciado em:
```
https://localhost:5001
```

---

## 🧪 Estrutura de Desenvolvimento em Equipe

Este projeto está sendo desenvolvido por um grupo de 6 colaboradores.  
Cada membro ficará responsável por uma **camada ou módulo específico** (por exemplo: Produtos, Pedidos, Clientes, Pagamentos, etc).

### 🧭 Fluxo de trabalho Git

1. Criar branch nova a partir da `main`:
   ```bash
   git checkout -b feature/nome-da-feature
   ```

2. Fazer commits organizados:
   ```bash
   git add .
   git commit -m "feat: adiciona entidade Produto"
   ```

3. Subir para o remoto:
   ```bash
   git push origin feature/nome-da-feature
   ```

4. Abrir **Pull Request** e aguardar revisão antes de mergear na `main`.

---

## 🧠 Padrões e Boas Práticas

- Seguir os princípios **SOLID**
- Separar responsabilidades em classes e interfaces
- Utilizar **injeção de dependência** para repositórios e serviços
- Criar **DTOs** para transporte de dados (não expor entidades diretamente)
- Aplicar **mapeamentos com AutoMapper**
- Documentar os endpoints com **Swagger**

---

## 👥 Colaboradores

| Nome | 
|------|
| Larissa Ferreira | 
| Carolina Diaz |
| Gabriel |
| Rodrigo |
| David |




---

## 🧾 Licença

Este projeto é de uso acadêmico e educacional.  
Distribuição livre mediante citação da fonte original.
