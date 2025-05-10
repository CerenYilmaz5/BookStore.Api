# 📚 BookStore API

A fully RESTful .NET 8 Web API project designed to manage books and authors. Built with a layered architecture and modern development best practices, this API supports user authentication (JWT), CRUD operations, DTO mapping, validation, and more.

---

## 🚀 Features

* **JWT Authentication** with fake login system
* **Role-based Authorization** (Normal & Fake)
* **CRUD operations** for:

   * Books
   * Authors
* **AutoMapper** for entity-DTO transformation
* **FluentValidation** for request validation
* **Custom Middlewares** for:

   * Logging incoming requests
   * Global exception handling
* **Swagger** integrated for API testing and documentation

---

## 🧱 Technologies Used

* .NET 8 Web API
* C#
* AutoMapper
* FluentValidation
* Swagger (Swashbuckle)
* JWT (System.IdentityModel.Tokens.Jwt)

---

## 📁 Folder Structure

```
BookStore.Api
├── Controllers          // API endpoints
├── DTOs                 // Data transfer objects (inputs/outputs)
├── Models               // Domain models/entities
├── Extensions           // AutoMapper extensions
├── Services             // Interfaces & in-memory service implementations
├── Validators           // FluentValidation classes
├── Middlewares          // Custom middleware (logging, error handling)
├── Configurations       // JWT settings
├── Program.cs
└── Startup.cs           // Service/middleware registration
```

---

## 🔐 Authentication

* `POST /api/authentication/login`

   * Body: `{ "username": "fake", "password": "any" }`
   * Roles:

      * **Fake**: Allowed to perform POST (create) operations
      * **Normal**: Read-only access

Use the returned JWT in the `Authorization: Bearer <token>` header.

---

## 📘 API Endpoints

### 📚 Book

* `GET /api/book`
* `GET /api/book/{id}`
* `POST /api/book` *(Fake only)*
* `PUT /api/book/{id}`
* `PATCH /api/book/{id}`
* `DELETE /api/book/{id}`
* `GET /api/book/list?title=...&sort=...`

### ✒️ Author

* `GET /api/author`
* `GET /api/author/{id}`
* `POST /api/author` *(Fake only)*
* `PUT /api/author/{id}`
* `DELETE /api/author/{id}` *(only if author has no books)*

---

## 🧪 Testing & Development

To run the application locally:

### 1. Restore and build the project

```bash
dotnet restore
dotnet build
```

### 2. Run the API

```bash
dotnet run
```

### 3. Open Swagger for testing endpoints

```
https://localhost:{port}/swagger
```

### 4. Use sample login

* Username: `fake`
* Password: `any`
* Copy the returned JWT and use it in `Authorize` button (top right of Swagger UI)

You can now test all endpoints, including role-restricted routes.

---

## 👩‍💻 Developer Notes

* Uses in-memory services (`FakeBookService`, `FakeAuthorService`)
* No EF Core or database dependency
* Designed for educational/demo purposes
* Clean code, SOLID principles, modular structure
