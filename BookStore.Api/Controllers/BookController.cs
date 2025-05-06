using BookStore.Api.Models;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Api.Controllers
{
    // Define the route for the controller as api/book
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        // A static list of books, simulating a database
        private static readonly List<Book> Books = new()
        {
            new Book { Id = 1, Title = "The Hunger Games", Author = "Suzanne Collins", Price = 45, Stock = 10, PageCount = 374, PublishedDate = new DateTime(2008, 9, 14) },
            new Book { Id = 2, Title = "Jane Eyre", Author = "Charlotte Brontë", Price = 55, Stock = 8, PageCount = 500, PublishedDate = new DateTime(1847, 10, 16) },
            new Book { Id = 3, Title = "Murder on the Orient Express", Author = "Agatha Christie", Price = 60, Stock = 5, PageCount = 256, PublishedDate = new DateTime(1934, 1, 1) }
        };

        private readonly IValidator<Book> _validator;

        // Inject the validator into the controller to validate book objects
        public BookController(IValidator<Book> validator)
        {
            _validator = validator;
        }

        // GET: api/book
        // Returns a list of all books in the system
        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                // Return all books with a 200 OK status
                return Ok(Books);
            }
            catch (Exception ex)
            {
                // Return a 500 Internal Server Error in case of an exception
                return StatusCode(500, new { status = 500, message = "Internal Server Error", detail = ex.Message });
            }
        }

        // GET: api/book/1
        // Returns a specific book based on the provided ID
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                var book = Books.FirstOrDefault(b => b.Id == id);
                if (book == null)
                    return NotFound(new { status = 404, message = "Book not found" });

                // Return the book found with a 200 OK status
                return Ok(book);
            }
            catch (Exception ex)
            {
                // Return a 500 Internal Server Error in case of an exception
                return StatusCode(500, new { status = 500, message = "Internal Server Error", detail = ex.Message });
            }
        }

        // POST: api/book
        // Adds a new book to the system
        [HttpPost]
        public IActionResult Create([FromBody] Book book)
        {
            try
            {
                // Validate the incoming book data using FluentValidation
                ValidationResult validationResult = _validator.Validate(book);
                if (!validationResult.IsValid)
                    return BadRequest(new { status = 400, errors = validationResult.Errors });

                // Generate a new ID for the book and add it to the list
                book.Id = Books.Any() ? Books.Max(b => b.Id) + 1 : 1;
                Books.Add(book);

                // Return a 201 Created status with a link to the newly created book
                return CreatedAtAction(nameof(GetById), new { id = book.Id }, book);
            }
            catch (Exception ex)
            {
                // Return a 500 Internal Server Error in case of an exception
                return StatusCode(500, new { status = 500, message = "Internal Server Error", detail = ex.Message });
            }
        }

        // PUT: api/book/1
        // Updates an existing book in the system
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Book book)
        {
            try
            {
                var existingBook = Books.FirstOrDefault(b => b.Id == id);
                if (existingBook == null)
                    return NotFound(new { status = 404, message = "Book not found" });

                // Validate the incoming book data using FluentValidation
                ValidationResult validationResult = _validator.Validate(book);
                if (!validationResult.IsValid)
                    return BadRequest(new { status = 400, errors = validationResult.Errors });

                // Update the existing book with new data
                existingBook.Title = book.Title;
                existingBook.Author = book.Author;
                existingBook.Price = book.Price;
                existingBook.Stock = book.Stock;
                existingBook.PageCount = book.PageCount;
                existingBook.PublishedDate = book.PublishedDate;

                // Return the updated book with a 200 OK status
                return Ok(existingBook);
            }
            catch (Exception ex)
            {
                // Return a 500 Internal Server Error in case of an exception
                return StatusCode(500, new { status = 500, message = "Internal Server Error", detail = ex.Message });
            }
        }

        // PATCH: api/book/1
        // Partially updates an existing book's details
        [HttpPatch("{id}")]
        public IActionResult PartiallyUpdate(int id, [FromBody] Book book)
        {
            try
            {
                var existingBook = Books.FirstOrDefault(b => b.Id == id);
                if (existingBook == null)
                    return NotFound(new { status = 404, message = "Book not found" });

                // Update only the provided fields
                if (!string.IsNullOrEmpty(book.Title)) existingBook.Title = book.Title;
                if (!string.IsNullOrEmpty(book.Author)) existingBook.Author = book.Author;
                if (book.Price > 0) existingBook.Price = book.Price;
                if (book.Stock >= 0) existingBook.Stock = book.Stock;
                if (book.PageCount > 0) existingBook.PageCount = book.PageCount;
                if (book.PublishedDate != default) existingBook.PublishedDate = book.PublishedDate;

                // Return the partially updated book with a 200 OK status
                return Ok(existingBook);
            }
            catch (Exception ex)
            {
                // Return a 500 Internal Server Error in case of an exception
                return StatusCode(500, new { status = 500, message = "Internal Server Error", detail = ex.Message });
            }
        }

        // DELETE: api/book/1
        // Deletes a specific book by ID
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var book = Books.FirstOrDefault(b => b.Id == id);
                if (book == null)
                    return NotFound(new { status = 404, message = "Book not found" });

                // Remove the book from the list
                Books.Remove(book);

                // Return a 204 No Content status, as the book has been deleted
                return NoContent();
            }
            catch (Exception ex)
            {
                // Return a 500 Internal Server Error in case of an exception
                return StatusCode(500, new { status = 500, message = "Internal Server Error", detail = ex.Message });
            }
        }

        // GET: api/book/list?title=abc
        // Filters and sorts books based on query parameters
        [HttpGet("list")]
        public IActionResult GetByQuery([FromQuery] string? title, [FromQuery] string? sort = null)
        {
            try
            {
                // Filter books by title if provided
                var filtered = Books
                    .Where(b => string.IsNullOrEmpty(title) || b.Title.Contains(title, StringComparison.OrdinalIgnoreCase))
                    .ToList();

                // Sort books based on the query parameter 'sort'
                if (!string.IsNullOrEmpty(sort))
                {
                    filtered = sort.ToLower() switch
                    {
                        "title" => filtered.OrderBy(b => b.Title).ToList(),
                        "price" => filtered.OrderBy(b => b.Price).ToList(),
                        "date" => filtered.OrderBy(b => b.PublishedDate).ToList(),
                        _ => filtered
                    };
                }

                // Return the filtered and/or sorted books
                return Ok(filtered);
            }
            catch (Exception ex)
            {
                // Return a 500 Internal Server Error in case of an exception
                return StatusCode(500, new { status = 500, message = "Internal Server Error", detail = ex.Message });
            }
        }
    }
}
