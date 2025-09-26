using AutoMapper;
using LibraryMS.API.Data.Entities;
using LibraryMS.API.Dtos;
using LibraryMS.API.Services;
using LibraryMS.API.Services.Interfaces;
using LibraryMS.API.UOW.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IBookService bookService;

        public BookController(IUnitOfWork unitOfWork, IMapper mapper, IBookService bookService)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.bookService = bookService;
        }

        [HttpPost]
        public async Task<ActionResult<Book>> Post([FromBody] CreateBookDto dto)
        {
            var library = await unitOfWork.Library.GetByIDAsync(dto.LibraryId);
            if (library == null) return BadRequest("Invalid Library ID");
            var book = mapper.Map<Book>(dto);
            book.AvailableCopies = dto.TotalCopies;
            book.ISBN = Guid.NewGuid().ToString()[..8];
            book.createdBy = 0;
            book.createdOn = DateTime.UtcNow;
            unitOfWork.Book.Insert(book);
            await unitOfWork.SaveAsync();

            return Ok(200);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var book = await bookService.GetByIdAsync(id);

            return book == null ? NotFound() : Ok(book);
        }

        [HttpGet]

        public async Task<IActionResult> Get()
        {
            var data = await bookService.GetAllAsync();
            return Ok(data);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] UpdateBookDto dto)
        {
            var existing = await unitOfWork.Book.GetByIDAsync(id);
            if (existing == null) return NotFound();
            var entity = mapper.Map(dto, existing);
            entity.updatedBy = 0;
            entity.UpdatedOn = DateTime.UtcNow;
            unitOfWork.Book.Update(entity);
            await unitOfWork.SaveAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var book = await unitOfWork.Book.GetByIDAsync(id);
            if (book == null || book.AvailableCopies != book.TotalCopies)
                return BadRequest("Cannot delete book with active loans or copies in use.");

            unitOfWork.Book.Delete(book);
            await unitOfWork.SaveAsync();

            return NoContent();
        }

        [HttpGet("available")]
        public async Task<ActionResult<IEnumerable<Book>>> GetAvailableBooks()
        {
            var books = await unitOfWork.Book
                .GetAsync(b => b.AvailableCopies > 0);

            return Ok(books);
        }

        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<BookDto>>> SearchBooks( string query,int? libraryId = null)
        {
            if (string.IsNullOrWhiteSpace(query))
                return BadRequest("Search query is required.");

            var results = await bookService.SearchBooksAsync(query.Trim(), libraryId);
            return Ok(results);
        }
    }
}
