using BookStore.Api.Attributes;
using BookStore.Api.Constants;
using BookStore.Api.Models;
using BookStore.Api.Services.Interfaces;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Api.Controllers
{
    // This controller handles all the HTTP requests related to books.
   

    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookService _service;
        private readonly IValidator<Book> _validator;

        // Inject the book service and the validator into this controller
        public BookController(IBookService service, IValidator<Book> validator)
        {
            _service = service;
            _validator = validator;
        }

        // GET: api/book
        [HttpGet]
        public IActionResult GetAll()
        {
            var books = _service.GetAll();
            return Ok(books);
        }

        // GET: api/book/{id}
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var book = _service.GetById(id);
            if (book == null)
                return NotFound(new { status = 404, message = "Book not found" });

            return Ok(book);
        }

        // POST: api/book
        // Only accessible by fake users for testing
        [HttpPost]
        [CustomAuthorize(UserRoles.Fake)]
        public IActionResult Create(Book book)
        {
            var result = _validator.Validate(book);
            if (!result.IsValid)
                return BadRequest(new { status = 400, errors = result.Errors });

            var created = _service.Create(book);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        // PUT: api/book/{id}
        [HttpPut("{id}")]
        public IActionResult Update(int id, Book book)
        {
            var result = _validator.Validate(book);
            if (!result.IsValid)
                return BadRequest(new { status = 400, errors = result.Errors });

            var updated = _service.Update(id, book);
            return updated == null
                ? NotFound(new { status = 404, message = "Book not found" })
                : Ok(updated);
        }

        // PATCH: api/book/{id}
        [HttpPatch("{id}")]
        public IActionResult Patch(int id, Book book)
        {
            var patched = _service.Patch(id, book);
            return patched == null
                ? NotFound(new { status = 404, message = "Book not found" })
                : Ok(patched);
        }

        // DELETE: api/book/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var deleted = _service.Delete(id);
            return deleted
                ? NoContent()
                : NotFound(new { status = 404, message = "Book not found" });
        }

        // GET: api/book/list?title=x&sort=price
        [HttpGet("list")]
        public IActionResult GetFiltered([FromQuery] string? title, [FromQuery] string? sort)
        {
            var result = _service.Filter(title, sort);
            return Ok(result);
        }
    }
}
