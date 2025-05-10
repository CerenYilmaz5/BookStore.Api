using BookStore.Api.DTOs;
using BookStore.Api.Extensions;
using BookStore.Api.Services.Interfaces;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Api.Controllers
{
    // Handles all HTTP requests related to author operations
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorService _service;
        private readonly IValidator<UpdateAuthorRequest> _validator;
        private readonly IValidator<GetByIdRequest> _idValidator;

        public AuthorController(
            IAuthorService service,
            IValidator<UpdateAuthorRequest> validator,
            IValidator<GetByIdRequest> idValidator)
        {
            _service = service;
            _validator = validator;
            _idValidator = idValidator;
        }

        // GET: api/author
        // Returns all authors
        [HttpGet]
        public IActionResult GetAll()
        {
            var authors = _service.GetAll().Select(a => a.ToResponse());
            return Ok(authors);
        }

        // GET: api/author/{id}
        // Returns author details by ID
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var validation = _idValidator.Validate(new GetByIdRequest { Id = id });
            if (!validation.IsValid)
                return BadRequest(new { status = 400, errors = validation.Errors });

            var author = _service.GetById(id);
            return author == null
                ? NotFound(new { status = 404, message = "Author not found" })
                : Ok(author.ToResponse());
        }

        // POST: api/author
        // Creates a new author (only Fake users allowed)
        [HttpPost]
        [Authorize(Roles = "Fake")]
        public IActionResult Create([FromBody] UpdateAuthorRequest request)
        {
            var validation = _validator.Validate(request);
            if (!validation.IsValid)
                return BadRequest(new { status = 400, errors = validation.Errors });

            var created = _service.Create(request.ToEntity());
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created.ToResponse());
        }

        // PUT: api/author/{id}
        // Updates an author's details completely
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] UpdateAuthorRequest request)
        {
            var idValidation = _idValidator.Validate(new GetByIdRequest { Id = id });
            if (!idValidation.IsValid)
                return BadRequest(new { status = 400, errors = idValidation.Errors });

            var validation = _validator.Validate(request);
            if (!validation.IsValid)
                return BadRequest(new { status = 400, errors = validation.Errors });

            var updated = _service.Update(id, request.ToEntity());
            return updated == null
                ? NotFound(new { status = 404, message = "Author not found" })
                : Ok(updated.ToResponse());
        }

        // DELETE: api/author/{id}
        // Deletes an author only if no books are assigned
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var validation = _idValidator.Validate(new GetByIdRequest { Id = id });
            if (!validation.IsValid)
                return BadRequest(new { status = 400, errors = validation.Errors });

            var success = _service.Delete(id);
            return success
                ? NoContent()
                : Conflict(new { status = 409, message = "Author cannot be deleted. Books exist under this author." });
        }
    }
}
