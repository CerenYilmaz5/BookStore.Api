# 📚 BookStore API

A simple, educational RESTful Web API built with ASP.NET Core.  
This project demonstrates the use of SOLID principles, dependency injection, validation, custom authorization, global exception handling, and Swagger documentation.

---

## ✅ Features

- RESTful architecture with full CRUD support for books  
- Fake vs Normal user login via token simulation  
- SOLID-principled service layer and controller design  
- Custom attribute-based authorization (`CustomAuthorize`)  
- FluentValidation integration for input validation  
- Global exception handling middleware  
- Request logging middleware  
- Swagger UI for easy API testing  

---

## 🔐 User Simulation

| User Type     | Header Example                   | Access                       |
|---------------|----------------------------------|------------------------------|
| Fake User     | `Authorization: fake-token`      | Can access `POST /api/book` |
| Normal User   | `Authorization: normal-token`    | Can access `GET`, `PUT`, etc. |
| Missing Token | –                                | ❌ Returns `401 Unauthorized` |

---

## 🚀 How to Run

1. Clone the repository  
2. Open in Rider / Visual Studio / VS Code  
3. Run the project  
4. Open Swagger UI: `https://localhost:{port}/swagger`  

---

## 📬 Sample POST Request (as Fake User)

- **Endpoint**: `POST /api/book`  
- **Header**:
  ```
  Authorization: fake-token
  ```
- **Body**:
```json
{
  "title": "Test Book",
  "author": "Test Author",
  "price": 49.99,
  "stock": 5,
  "pageCount": 320,
  "publishedDate": "2023-01-01"
}
```

---

## 📌 Technologies Used

- .NET 8 Web API  
- C#  
- FluentValidation  
- Swagger / OpenAPI  
- Dependency Injection  
- Custom Middleware  

---

## 📡 API Endpoints

### 📖 Books

| Method | Endpoint            | Description        | Authorization |
|--------|---------------------|--------------------|----------------|
| GET    | `/api/book`         | List all books     | Normal User    |
| GET    | `/api/book/{id}`    | Get specific book  | Normal User    |
| POST   | `/api/book`         | Create new book    | Fake User      |
| PUT    | `/api/book/{id}`    | Update book        | Normal User    |
| DELETE | `/api/book/{id}`    | Delete book        | Normal User    |

---

## 🧪 Request Examples

### 🔹 Get All Books

```http
GET /api/book
Authorization: normal-token
```

### 🔹 Get Single Book

```http
GET /api/book/1
Authorization: normal-token
```

### 🔹 Update Book

```http
PUT /api/book/1
Authorization: normal-token
Content-Type: application/json

{
  "title": "Updated Book",
  "author": "Updated Author",
  "price": 59.99,
  "stock": 10,
  "pageCount": 300,
  "publishedDate": "2023-01-01"
}
```

---

## ✅ Response Examples

### 🔹 Successful Response

```json
{
  "id": 1,
  "title": "Test Book",
  "author": "Test Author",
  "price": 49.99,
  "stock": 5,
  "pageCount": 320,
  "publishedDate": "2023-01-01"
}
```

### 🔹 Error Response

```json
{
  "status": 401,
  "message": "Access denied. Required token: 'fake-token'."
}
```

---

## ⚠️ Error Codes

| Code | Description         | Suggested Solution                 |
|------|---------------------|------------------------------------|
| 400  | Bad Request         | Check your request format and data |
| 401  | Unauthorized        | Verify your authentication token   |
| 404  | Not Found           | Check if the resource ID is valid  |
| 429  | Too Many Requests   | Wait before retrying               |
| 500  | Server Error        | Contact the system administrator   |

---

## 📝 Request Validation

- **Title**: Required, max 100 characters  
- **Author**: Required, max 100 characters  
- **Price**: Required, must be greater than 0  
- **Stock**: Required, must be 0 or more  
- **Page Count**: Required, must be greater than 0  
- **Published Date**: Required, format: `YYYY-MM-DD`
