# Cadastro de Empresas com Autenticação e Consulta via CNPJ (ReceitaWS)

Este projeto é uma API desenvolvida em ASP.NET Core com foco em boas práticas de arquitetura e organização de código, permitindo o **cadastro e autenticação de usuários**, bem como o **cadastro de empresas por meio da consulta ao CNPJ** utilizando a API pública da ReceitaWS.

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

---

## 🚀 Como rodar o projeto

1. Clone o repositório
2. Configure a connection string no `appsettings.json`
3. Rode as migrations com `Update-Database`
4. Execute o projeto via Visual Studio
5. Use o Postman para testar as rotas

---

## 📎 Observações

Este projeto está em andamento e serve tanto como avaliação técnica quanto como estudo prático de arquitetura limpa e integração com APIs públicas.

