# 📚 BookStore API

A RESTful Web API for managing a collection of books.  
Built with **.NET 8**, designed with **SOLID principles**, and secured using **JWT authentication**.

---

## 🚀 Features

- ✅ CRUD operations for books
- 🔐 JWT-based authentication
- 👤 Role-based authorization (`Fake` vs `Normal`)
- 🧪 In-memory fake data service
- 📄 DTO usage for input/output models
- ⚠️ Global exception handling
- 📋 FluentValidation-based request validation
- 📜 Swagger UI documentation
- 📝 Simple request logging middleware

---

## 🛠️ How to Run

1. Clone the repo
2. Restore packages  
   `dotnet restore`
3. Run the app  
   `dotnet run`
4. Visit Swagger UI  
   `https://localhost:<port>/swagger`

---

## 🔑 Authentication

This API uses **JWT tokens** to authenticate users.

To log in and get a token:

```
POST /api/auth/login
Content-Type: application/json

{
  "username": "fake",
  "password": "any"
}
```

- `fake` username gives you **Fake** role.
- All other usernames get the **Normal** role.

Use the token in request headers like:

```
Authorization: Bearer YOUR_JWT_TOKEN
```

---

## 🔐 Role Access

| Endpoint         | Role Required   |
|------------------|-----------------|
| GET /api/book     | Any             |
| POST /api/book    | Fake only       |
| PUT/PATCH/DELETE  | Any             |

---

## 📚 Example Book Object

```json
{
  "id": 1,
  "title": "The Hobbit",
  "author": "J.R.R. Tolkien",
  "price": 45.00,
  "stock": 5,
  "pageCount": 320,
  "publishedDate": "1937-09-21"
}
```

---

## 🧪 Test Scenarios (JWT)

### 1. 🔐 Login – Get Token

```
POST /api/auth/login
{
  "username": "fake",
  "password": "1234"
}
```

Response:

```json
{
  "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...",
  "username": "fake",
  "role": "Fake",
  "expiresIn": 60
}
```

### 2. 📎 Use Token in Headers

```
Authorization: Bearer YOUR_JWT_TOKEN
```

### 3. 📚 Get Books

```
GET /api/book
```

### 4. 📝 Create Book

```
POST /api/book
{
  "title": "Clean Code",
  "author": "Robert C. Martin",
  "price": 85,
  "stock": 5,
  "pageCount": 464,
  "publishedDate": "2008-08-11"
}
```

### 5. ✏️ Update Book

```
PUT /api/book/1
{
  "title": "Updated Title",
  "author": "Updated Author",
  "price": 100,
  "stock": 10,
  "pageCount": 500,
  "publishedDate": "2020-01-01"
}
```

---

## 📎 ID Validation Filter

This API uses a reusable filter to validate all route parameters named `id`.

### ✅ What It Does

- Ensures `id` is a **positive integer**
- Returns `400 Bad Request` if invalid

### 💡 Example

```
GET /api/book/-5
```

Response:

```json
{
  "status": 400,
  "message": "Invalid id. Must be a positive integer."
}
```

---

## ✅ Validation & Clean Architecture Notes

This API follows best practices by using DTOs and FluentValidation. Here's how:

### 📥 DTO Usage (Input/Output Models)
- `UpdateBook` uses `UpdateBookRequest` for input and `BookResponse` for output.
- `GetById` uses a validated `int id` (via `GetByIdRequest`) and returns `BookResponse`.
- ❌ At no point is the internal `Book` entity exposed directly to the client.

### 🔍 FluentValidation Rules

| Operation   | Validator Class              | Validates                        |
|-------------|------------------------------|-----------------------------------|
| Update      | `UpdateBookRequestValidator` | Title, Author, Price, etc.       |
| GetById     | `GetByIdRequestValidator`    | Ensures `id > 0`                 |
| Delete      | `GetByIdRequestValidator`    | Shared validator with GetById    |

### 🧪 Controller Enforcement

```csharp
var result = _updateValidator.Validate(request);
if (!result.IsValid)
    return BadRequest(result.Errors);
```

---

## 🗂️ Project Folder Structure

```
BookStore.Api
│
├── Configurations/                    # Configuration classes (e.g., JWT settings)
│   └── JwtSettings.cs
│
├── Controllers/                       # API endpoint controllers
│   ├── AuthController.cs
│   └── BookController.cs
│
├── DTOs/                              # Request and response models (Data Transfer Objects)
│   ├── BookResponse.cs
│   ├── GetByIdRequest.cs
│   ├── TokenResponse.cs
│   ├── UpdateBookRequest.cs
│   └── UserLoginRequest.cs
│
├── Extensions/                        # Extension methods and mappings
│   └── MappingExtensions.cs
│
├── Middlewares/                       # Custom middleware for exception handling and logging
│   ├── ExceptionHandlingMiddleware.cs
│   └── RequestLoggingMiddleware.cs
│
├── Models/                            # Domain entities
│   └── Book.cs
│
├── Services/                          # Application service layer
│   ├── Interfaces/                    # Service contracts
│   │   ├── IBookService.cs
│   │   └── IJwtService.cs
│   └── Implementations/              # Service implementations
│       ├── FakeBookService.cs
│       └── JwtService.cs
│
├── Validators/                        # FluentValidation rules
│   ├── GetByIdRequestValidator.cs
│   └── UpdateBookRequestValidator.cs
│
├── Program.cs                         # Entry point of the application
├── Startup.cs                         # Central configuration for services and middleware
├── appsettings.json                   # Configuration file for JWT and app settings

```


