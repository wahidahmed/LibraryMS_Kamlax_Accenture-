using AutoMapper;
using LibraryMS.API.Data.Entities;
using LibraryMS.API.Dtos;
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

        public BookController(IUnitOfWork unitOfWork,IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult<Book>> Post([FromBody] CreateBookDto dto)
        {
            var library = await unitOfWork.Library.GetByIDAsync(dto.LibraryId);
            if (library == null) return BadRequest("Invalid Library ID");
            var book=mapper.Map<Book>(dto);
            book.AvailableCopies=dto.TotalCopies;
            book.ISBN = Guid.NewGuid().ToString()[..8];
            book.createdBy = 0;
            book.createdOn = DateTime.UtcNow;
            unitOfWork.Book.Insert(book);
            await unitOfWork.SaveAsync();

            return CreatedAtAction(nameof(Get), new { id = book.BookId }, book);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Book>> Get(int id)
        {
            var book = await unitOfWork.Book
                .GetFirstOrDefaultAsync(b => b.BookId == id);

            return book == null ? NotFound() : Ok(book);
        }

        [HttpGet]

        public async Task<IActionResult> Get()
        {
            var data=await unitOfWork.Book.GetAsync();
            return Ok(data);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] UpdateBookDto dto)
        {
            var existing = await unitOfWork.Book.GetByIDAsync(id);
            if (existing == null) return NotFound();
            var entity=mapper.Map(dto,existing);
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
    }
}
