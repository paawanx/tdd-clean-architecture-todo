## 🧱 ToDoApp – Clean Architecture with TDD & CQRS

A **.NET 8 Web API** built using **Clean Architecture**, **CQRS (with MediatR)**, and **Test-Driven Development (TDD)**.
This project demonstrates how to design maintainable, testable systems with clear separation of concerns.

---

### 🧩 Architecture Overview

```
ToDoApp.sln
│
├── ToDoApp.Api            → Presentation layer (Minimal API + MediatR)
├── ToDoApp.Application    → CQRS layer (commands, handlers, interfaces)
├── ToDoApp.Core           → Domain layer (entities, value objects, Result<T>)
├── ToDoApp.Infrastructure → Infrastructure layer (repository implementation)
└── ToDoApp.Tests          → Unit & integration tests
```

* **Core** – Domain logic and validation (`Todo`, `Result<T>`).
* **Application** – CQRS commands, handlers, and interfaces (`CreateTodoCommandHandler`, `ITodoRepository`).
* **Infrastructure** – Pluggable persistence (currently in-memory).
* **Api** – Minimal API endpoint using MediatR.
* **Tests** – xUnit, FluentAssertions, Moq; covers both unit and API-level behavior.

---

### 🧠 Key Concepts Implemented

* **Test-Driven Development (TDD)** – all logic written from failing tests (red → green → refactor).
* **CQRS + MediatR** – commands/queries handled via request–response pattern.
* **Clean Architecture** – domain at the center, infrastructure on the outside.
* **Dependency Injection** – repository abstractions injected at runtime.
* **Integration Testing** – API verified end-to-end using in-memory `WebApplicationFactory`.

---

### ⚙️ Run & Test

Run the API:

```bash
dotnet run --project ToDoApp.Api
```

Run all tests:

```bash
dotnet test
```

Try the endpoint:

```bash
curl -X POST https://localhost:5001/todos \
     -H "Content-Type: application/json" \
     -d '{"title": "Buy milk"}'
```

Response:

```json
{ "id": "generated-guid-here" }
```

---

### ✅ What’s Included

* `Todo` entity with validation
* Generic `Result<T>` for clean error handling
* `CreateTodoCommand` + `CreateTodoCommandHandler`
* Minimal API endpoint `/todos`
* In-memory repository
* Unit & integration tests demonstrating full workflow

---

### 📚 Goal

This project was built to **learn and review modern .NET backend design**, focusing on:

* Applying TDD effectively
* Structuring Clean Architecture solutions
* Using CQRS with MediatR in a minimal API setup
* Writing maintainable, testable code with clear boundaries

