# Cadastro de Empresas com Autenticação e Consulta via CNPJ (ReceitaWS)

Este projeto é uma API desenvolvida em ASP.NET Core com foco em boas práticas de arquitetura em camadas e organização de código, permitindo o **cadastro e autenticação de usuários**, bem como o **cadastro de empresas por meio da consulta ao CNPJ** utilizando a API pública da ReceitaWS.

---

## 🎯 Objetivo

O principal objetivo da aplicação é permitir que usuários autenticados possam cadastrar empresas informando apenas o número do CNPJ, com os dados sendo automaticamente preenchidos a partir da ReceitaWS. O projeto também visa demonstrar a aplicação de conceitos como:

- Autenticação com JWT
- Armazenamento seguro de senhas (BCrypt)
- Clean Architecture com separação em camadas
- Boas práticas de validação e tratamento de erros
- Integração com serviços externos via `HttpClient`

---

## 🧱 Estrutura de Pastas

A solução está organizada nas seguintes camadas:

- **WebAPI**: Camada de apresentação, onde estão os controllers e o `Program.cs`
- **Application**: Serviços de aplicação e lógica de negócio implementada
- **Business**: Interfaces, DTOs e modelos de domínio
- **Infrastructure**: Acesso a dados, Entity Framework, Migrations e serviços externos

---

## ✅ Funcionalidades já implementadas

- Cadastro de usuários com criptografia de senha
- Autenticação com JWT
- Proteção de endpoints com `[Authorize]`
- Cadastro de empresas a partir do CNPJ via API ReceitaWS
- Validação dos dados recebidos da ReceitaWS
- Organização da solução em camadas
- Início da refatoração dos controllers para utilizar os serviços de negócio

---

## 📌 Tecnologias utilizadas

- ASP.NET Core 8.0
- Entity Framework Core
- SQL Server
- JWT (Json Web Token)
- BCrypt.Net
- Postman
- FluentValidation
- ReceitaWS API
- Camadas: WebAPI, Application, Business, Infrastructure
- 
---

## 🚀 Como rodar o projeto

### Pré-requisitos
- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- SQL Server (localdb, express ou outro)
- Conta gratuita em [ReceitaWS](https://www.receitaws.com.br/)

### Etapas

1. Clone o repositório:
   ```bash
   git clone https://github.com/seu-usuario/CadastroDeEmpresa.git
   ```

2. Configure o `appsettings.json`:
   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=CadastroDeEmpresa;Trusted_Connection=True;"
     },
     "Jwt": {
       "Key": "sua-chave-secreta-jwt",
       "Issuer": "CadastroEmpresaAPI",
       "Audience": "CadastroEmpresaAPI"
     },
     "ReceitaWS": {
       "Token": "seu-token-da-receita-aqui"
     }
   }
   ```

3. Execute os comandos:
   ```bash
   dotnet ef database update
   dotnet run --project WebAPI
---

## 🚀 Funcionalidades no Postman

- `POST /api/auth/register` → Registro de usuário
- `POST /api/auth/login` → Login e geração de token JWT
- `GET /api/usuario/authenticate` → Validação do token e exibição dos dados do usuário
- `POST /api/company/register` → Cadastro de empresa por CNPJ
- `GET /api/company/list` → Listagem das empresas do usuário autenticado


## 📎 Observações

Este projeto está em andamento e serve tanto como avaliação técnica quanto como estudo prático de arquitetura limpa e integração com APIs públicas.

