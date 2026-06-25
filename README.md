# Beck Support Bot

## Description

Beck Support Bot is an AI-powered support application built with ASP.NET Core Web API and the OpenAI API.

The application combines a local knowledge base with OpenAI's language models to provide accurate and context-aware answers to user questions. Before sending a request to the AI model, the application searches relevant documentation and includes it as context, improving the quality and reliability of the generated responses.

The project demonstrates how Retrieval-Augmented Generation (RAG) concepts can be implemented in a .NET application to create an intelligent support assistant.

---

## Features

- AI-powered support assistant
- OpenAI API integration
- Local knowledge base
- Context-aware AI responses
- REST API
- Swagger / OpenAPI
- Dependency Injection
- Error handling
- JSON communication

---

## Technologies

### Backend
- C#
- .NET 8
- ASP.NET Core Web API
- Entity Framework Core
- Dependency Injection

### AI
- OpenAI API
- Prompt Engineering
- Context Retrieval
- Retrieval-Augmented Generation (RAG)

### Other Technologies
- REST API
- JSON
- Swagger / OpenAPI

---

## Project Structure

```text
BeckSupportBot
│
├── Controllers
├── Interfaces
├── Models
├── Services
├── KnowledgeBase
├── wwwroot
├── Program.cs
└── appsettings.json
```

---

## How It Works

1. A user submits a question through the API.
2. The Knowledge Service searches the local knowledge base for relevant documentation.
3. The most relevant documents are selected and combined into a context.
4. The context and the user's question are sent to the OpenAI API.
5. The AI generates a response based on both the question and the retrieved documentation.
6. The API returns the response as JSON.

This approach improves answer accuracy by grounding the AI in domain-specific documentation rather than relying solely on the language model.

---

## Installation

Clone the repository

```bash
git clone https://github.com/Dasekan/BeckSupportBot.git
```

Navigate to the project

```bash
cd BeckSupportBot
```

Restore NuGet packages

```bash
dotnet restore
```

Configure your OpenAI API key in **appsettings.json** or using **User Secrets**.

Run the application

```bash
dotnet run
```

Open Swagger

```
https://localhost:xxxx/swagger
```

---

## Skills Demonstrated

This project demonstrates experience with:

- ASP.NET Core Web API
- REST API development
- OpenAI API integration
- AI-assisted software development
- Retrieval-Augmented Generation (RAG)
- Dependency Injection
- Object-Oriented Programming
- JSON communication
- Knowledge retrieval
- Prompt Engineering
- Error handling

---

## Future Improvements

- Semantic Search using Embeddings
- Vector Database integration
- Authentication & Authorization
- Response caching
- Docker support
- Unit Tests
- Logging & Monitoring
- Azure deployment

---

## Purpose

The purpose of this project was to explore how AI can be integrated into modern .NET applications by combining a large language model with a local knowledge base. The project focuses on building a scalable and maintainable support assistant capable of delivering accurate, context-aware responses.

---

## Developed by

**Dasekan Allan Karim**
