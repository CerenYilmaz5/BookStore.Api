using BookStore.Api.DTOs;
using BookStore.Api.Extensions;
using BookStore.Api.Services.Interfaces;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Api.Controllers
{
    // This controller handles all HTTP requests related to books.
    // It is protected by JWT authentication and uses DTOs and validation for clean API behavior.
    [Route("api/[controller]")]
    [ApiController]
    [Authorize] // Require JWT token for all actions
    public class BookController : ControllerBase
    {
        private readonly IBookService _service;
        private readonly IValidator<UpdateBookRequest> _updateValidator;
        private readonly IValidator<GetByIdRequest> _idValidator;

        // Constructor with dependency injection of services and validators
        public BookController(
            IBookService service,
            IValidator<UpdateBookRequest> updateValidator,
            IValidator<GetByIdRequest> idValidator)
        {
            _service = service;
            _updateValidator = updateValidator;
            _idValidator = idValidator;
        }

        // GET: api/book
        // Returns all books in the system
        [HttpGet]
        public IActionResult GetAll()
        {
            var books = _service.GetAll().Select(b => b.ToResponse());
            return Ok(books);
        }

        // GET: api/book/5
        // Returns the book with the given ID
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var validation = _idValidator.Validate(new GetByIdRequest { Id = id });
            if (!validation.IsValid)
                return BadRequest(new { status = 400, errors = validation.Errors });

            var book = _service.GetById(id);
            if (book == null)
                return NotFound(new { status = 404, message = "Book not found" });

            return Ok(book.ToResponse());
        }

        // POST: api/book
        // Creates a new book. Only accessible by users with role "Fake"
        [HttpPost]
        [Authorize(Roles = "Fake")]
        public IActionResult Create([FromBody] UpdateBookRequest request)
        {
            var validation = _updateValidator.Validate(request);
            if (!validation.IsValid)
                return BadRequest(new { status = 400, errors = validation.Errors });

            var book = request.ToEntity();
            var created = _service.Create(book);

            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created.ToResponse());
        }

        // PUT: api/book/5
        // Fully updates the book with the given ID
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] UpdateBookRequest request)
        {
            var idValidation = _idValidator.Validate(new GetByIdRequest { Id = id });
            if (!idValidation.IsValid)
                return BadRequest(new { status = 400, errors = idValidation.Errors });

            var validation = _updateValidator.Validate(request);
            if (!validation.IsValid)
                return BadRequest(new { status = 400, errors = validation.Errors });

            var book = request.ToEntity();
            var updated = _service.Update(id, book);

            return updated == null
                ? NotFound(new { status = 404, message = "Book not found" })
                : Ok(updated.ToResponse());
        }

        // PATCH: api/book/5
        // Partially updates the book with the given ID
        [HttpPatch("{id}")]
        public IActionResult Patch(int id, [FromBody] UpdateBookRequest request)
        {
            var idValidation = _idValidator.Validate(new GetByIdRequest { Id = id });
            if (!idValidation.IsValid)
                return BadRequest(new { status = 400, errors = idValidation.Errors });

            var book = request.ToEntity();
            var patched = _service.Patch(id, book);

            return patched == null
                ? NotFound(new { status = 404, message = "Book not found" })
                : Ok(patched.ToResponse());
        }

        // DELETE: api/book/5
        // Deletes the book with the given ID
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var validation = _idValidator.Validate(new GetByIdRequest { Id = id });
            if (!validation.IsValid)
                return BadRequest(new { status = 400, errors = validation.Errors });

            var deleted = _service.Delete(id);
            return deleted
                ? NoContent()
                : NotFound(new { status = 404, message = "Book not found" });
        }

        // GET: api/book/list?title=abc&sort=price
        // Filters books by title and sorts the result
        [HttpGet("list")]
        public IActionResult GetFiltered([FromQuery] string? title, [FromQuery] string? sort)
        {
            var result = _service.Filter(title, sort).Select(b => b.ToResponse());
            return Ok(result);
        }
    }
}
