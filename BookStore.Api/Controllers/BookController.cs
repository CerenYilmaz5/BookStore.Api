using BookStore.Api.DTOs;
using BookStore.Api.Extensions;
using BookStore.Api.Services.Interfaces;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace BookStore.Api.Controllers
{
    // Handles all HTTP requests related to book operations
    [Route("api/[controller]")]
    [ApiController]
    [Authorize] // JWT authentication required
    public class BookController : ControllerBase
    {
        private readonly IBookService _service;
        private readonly IValidator<UpdateBookRequest> _updateValidator;
        private readonly IValidator<GetByIdRequest> _idValidator;

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
        // Returns all books
        [HttpGet]
        public IActionResult GetAll()
        {
            var books = _service.GetAll().Select(b => b.ToResponse());
            return Ok(books);
        }

        // GET: api/book/{id}
        // Returns a book by ID
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var validation = _idValidator.Validate(new GetByIdRequest { Id = id });
            if (!validation.IsValid)
                return BadRequest(new { status = 400, errors = validation.Errors });

            var book = _service.GetById(id);
            return book == null
                ? NotFound(new { status = 404, message = "Book not found" })
                : Ok(book.ToResponse());
        }

        // POST: api/book
        // Creates a new book (Fake users only)
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

        // PUT: api/book/{id}
        // Updates a book completely
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] UpdateBookRequest request)
        {
            var idValidation = _idValidator.Validate(new GetByIdRequest { Id = id });
            if (!idValidation.IsValid)
                return BadRequest(new { status = 400, errors = idValidation.Errors });

            var validation = _updateValidator.Validate(request);
            if (!validation.IsValid)
                return BadRequest(new { status = 400, errors = validation.Errors });

            var updated = _service.Update(id, request.ToEntity());

            return updated == null
                ? NotFound(new { status = 404, message = "Book not found" })
                : Ok(updated.ToResponse());
        }

        // PATCH: api/book/{id}
        // Partially updates a book
        [HttpPatch("{id}")]
        public IActionResult Patch(int id, [FromBody] UpdateBookRequest request)
        {
            var idValidation = _idValidator.Validate(new GetByIdRequest { Id = id });
            if (!idValidation.IsValid)
                return BadRequest(new { status = 400, errors = idValidation.Errors });

            var patched = _service.Patch(id, request.ToEntity());

            return patched == null
                ? NotFound(new { status = 404, message = "Book not found" })
                : Ok(patched.ToResponse());
        }

        // DELETE: api/book/{id}
        // Deletes a book
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
        // Returns filtered/sorted books
        [HttpGet("list")]
        public IActionResult GetFiltered([FromQuery] string? title, [FromQuery] string? sort)
        {
            var result = _service.Filter(title, sort).Select(b => b.ToResponse());
            return Ok(result);
        }
    }
}
