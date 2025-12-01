# 🛒 E-commerce — Backend (.NET) + Frontend (React)

Projeto acadêmico de um sistema de e-commerce, composto por uma API em C# .NET e um frontend em React (Vite).  
A persistência foi implementada via arquivos **JSON**, conforme orientação do professor.

---

## 📦 Tecnologias Utilizadas

### Backend

- C# .NET  
- API REST  
- Domain-Driven Design (DDD)  
- Persistência com arquivos JSON (não utiliza Entity Framework)

### Frontend

- React + Vite  
- TypeScript  
- Integração com API via função `apiFetch`  
- Variáveis de ambiente para URL do backend  

---

## 🚀 Como Executar o Projeto

### 1. Backend (.NET)

**Pré-requisitos:**

- .NET SDK 6 ou superior instalado

**Comandos:**

```bash
cd backend/Eccomerce_POO/src/AppEcommerce.API
dotnet restore
dotnet run
````

A API será iniciada nos seguintes endereços:

* [https://localhost:5001](https://localhost:5001)
* [http://localhost:5000](http://localhost:5000)

---

## 🧩 Configuração do Frontend (Variáveis de Ambiente)

O frontend usa **Vite**. Portanto, todas as variáveis de ambiente devem começar com:

* `VITE_`

**Local do arquivo:**

```text
frontend/shop-verde-dorado/.env.local
```

**Conteúdo obrigatório:**

```env
VITE_API_BASE_URL=https://localhost:5001
```

**Observação:**
Se o backend estiver rodando sem HTTPS (porta 5000), use:

```env
VITE_API_BASE_URL=http://localhost:5000
```

---

## ▶️ Executando o Frontend

```bash
cd  frontend\src
npm install
npm run dev
```

O frontend estará disponível em:

* [http://localhost:3000](http://localhost:3000)

---

## 🔗 Comunicação entre Front e Backend

Todas as requisições no frontend utilizam a função `apiFetch(endpoint)`.

**Exemplo de chamada:**

```ts
apiFetch("/api/produtos");
```

* A base da URL é definida no arquivo `.env.local` (`VITE_API_BASE_URL`).
* O `endpoint` corresponde às rotas REST do backend.

---

## 🧠 Arquitetura do Backend (DDD)

O projeto segue **Domain-Driven Design (DDD)**, com separação clara de responsabilidades.

### Camadas principais

#### 1. Domain

Responsável pelas regras de negócio e entidades.
Não conhece JSON, HTTP, persistência ou banco.

Exemplos de entidades:

* `Produto`
* `Cliente`
* `Carrinho`
* `Pedido`

#### 2. Application

Implementa casos de uso e lógica do negócio.
Recebe entidades, valida dados, chama repositórios via interfaces.

Exemplos:

* `CarrinhoService`
* `PedidoService`

#### 3. Infra

Responsável por persistência e infraestrutura.
Neste projeto, implementada usando **arquivos JSON**.
Os repositórios leem e escrevem JSON utilizando `System.IO`.

#### 4. API

Interface HTTP exposta ao usuário:

* Controllers
* Endpoints REST
* Conversão de DTOs
* Chamada de serviços da camada Application

---

## 📂 Estrutura Geral do Projeto

```text
Eccomerce_POO/
  ├── Domain/          # entidades e contratos
  ├── Application/     # serviços e casos de uso
  ├── Infra/           # repositórios com JSON
  └── API/             # controllers REST

shop-verde-dorado/
  ├── ...              # código do frontend React + Vite
  └── .env.local       # configuração da URL da API
```

---

## 🗃️ Persistência com JSON

A persistência foi alterada do planejamento original com Entity Framework
para arquivos **JSON**, atendendo recomendação acadêmica.

**Exemplo de arquivo `produtos.json`:**

```json
[
  {
    "idProduto": 1,
    "nome": "Camiseta Básica",
    "preco": 29.90,
    "estoque": 10,
    "categoria": "Moda"
  }
]
```

### Benefícios da abordagem com JSON

* Não exige banco de dados instalado
* Não usa migrations
* Facilita a avaliação pelo professor
* Execução simples, sem dependências externas complexas

---

## ✍️ Autoria

Projeto desenvolvido por **Larissa Ferreira** para fins acadêmicos.

```
::contentReference[oaicite:0]{index=0}
```