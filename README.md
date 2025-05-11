# 📚 BookStore API

A clean, modular, and test-driven RESTful API built with **ASP.NET Core 8**, designed to manage books, authors, and genres. The project adheres to **SOLID principles**, leverages **JWT-based authentication**, and emphasizes **separation of concerns**, **input validation**, and **unit test coverage**.

---

## 🚀 Core Features

- 📘 **Book CRUD** with filtering, sorting, and genre support
- 👤 **Author CRUD** with business rule: prevent deletion if books exist
- 🏷️ **Genre CRUD** operations (with tests implemented)
- 🔐 **JWT-based authentication** with role-based access
- 🔄 **AutoMapper** for DTO ↔ Entity mapping
- ✔️ **FluentValidation** for request model validation
- ⚠️ **Global exception handling** and **request logging middlewares**
- 🧪 Fully structured **unit tests** using xUnit and test helpers
- 🧠 **In-memory fake services** (no external DB dependency)

---

## ⚙️ Technologies Used

| Tool / Library            | Purpose                                      |
|---------------------------|----------------------------------------------|
| .NET 8 Web API            | Framework                                    |
| AutoMapper                | DTO ↔ Entity mapping                         |
| FluentValidation          | Model validation                             |
| xUnit                     | Unit testing                                 |
| JWT                       | Authentication/Authorization                 |
| Swagger (Swashbuckle)     | API testing/documentation                    |
| In-memory services        | Fake services for test/demo environments     |

---

## 📁 Folder Structure

```
BookStore.Api/
│
├── Controllers/            # API endpoints (Book, Author, Genre)
├── DTOs/                   # Input and output models
├── Models/                 # Core domain models
├── Extensions/             # Mapping logic (AutoMapper extensions)
├── Services/               # Service interfaces and fake implementations
├── Validators/             # FluentValidation rule sets
├── Middlewares/            # Logging and exception middlewares
├── Configurations/         # JWT and other app configuration classes
├── Program.cs / Startup.cs # Entry point and service registration

BookStore.Tests/
│
├── Book/                   # Unit tests for Book services & validators
├── Genre/                  # Unit tests for Genre services & validators
├── Author/                 # Unit tests for Author services & rules
```

---

## 🔐 Authentication

- Use `POST /api/authentication/login` to get a JWT token.
- Logging in as `"fake"` user returns the `"Fake"` role.
- All other users default to `"Normal"` role.
- Access rules:
    - `"Fake"` users can access limited endpoints (e.g., create-only)
    - `"Normal"` users can access full functionality

---

## ✅ Business Rules

- Authors **cannot be deleted** if they have associated books.
- Book `PublishedDate` must **not be in the future**.
- All essential fields (`Title`, `Author`, `Genre`, etc.) are **required and validated**.
- `Genre` field added to Book model and supported across all layers and filters.

---

## 🧪 Unit Testing Guide

- All tests are written with **xUnit** and **FluentValidation.TestHelper**
- Follows best practices: Arrange–Act–Assert pattern
- Validator and service logic are tested separately

### ▶ Running all tests:

```bash
dotnet test BookStore.Tests
```

✅ All tests follow naming conventions:
- `XCommandTests.cs` → core logic test
- `XCommandValidatorTests.cs` → validation rule test

---

## 🔧 Project Setup

```bash
dotnet restore
dotnet build
dotnet run --project BookStore.Api
```

Then open:

```
https://localhost:5001/swagger
```

to explore and test the API via Swagger UI.

---

## 🗓️ Future Enhancements

- GenreController with full REST operations
- Real data store integration (EF Core / Dapper)
- Docker support for deployment
- Centralized logging to file
- CI/CD pipeline for tests (GitHub Actions)
