using LibraryMS.API.Data;
using LibraryMS.API.Dtos;
using LibraryMS.API.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LibraryMS.API.Services
{
    public class BookService: IBookService
    {
        private readonly AppDbContext _context;

        public BookService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<BookDto> GetByIdAsync(int id)
        {
            return await _context.Books
                .Where(b => b.BookId == id)
                .Include(b => b.Library) // Needed to get LibraryName
                .Select(b => new BookDto
                {
                    BookId= b.BookId,
                    LibraryId= b.LibraryId,
                    Title = b.Title,
                    Author = b.Author,
                    ISBN = b.ISBN,
                    TotalCopies = b.TotalCopies,
                    AvailableCopies = b.AvailableCopies,
                    LibraryName=b.Library.LibraryName
                }
                )
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<BookDto>> GetAllAsync()
        {
            return await _context.Books
                .Include(b => b.Library)
                .Select(b => new BookDto
                {
                    BookId = b.BookId,
                    LibraryId = b.LibraryId,
                    Title = b.Title,
                    Author = b.Author,
                    ISBN = b.ISBN,
                    TotalCopies = b.TotalCopies,
                    AvailableCopies = b.AvailableCopies,
                    LibraryName = b.Library.LibraryName
                }
            ).ToListAsync();
        }

        public async Task<IEnumerable<BookDto>> SearchBooksAsync(string query, int? libraryId = null)
        {
            var queryBuilder = _context.Books.AsQueryable();

            if (libraryId.HasValue)
                queryBuilder = queryBuilder.Where(b => b.LibraryId == libraryId.Value);

            queryBuilder = queryBuilder
                .Include(b => b.Library) // To get LibraryName
                .Where(b =>
                    EF.Functions.Like(b.Title, $"%{query}%") ||
                    EF.Functions.Like(b.Author, $"%{query}%") ||
                    EF.Functions.Like(b.ISBN, $"%{query}%")
                );

            return await queryBuilder
                .Select(b => new BookDto
                {
                    BookId = b.BookId,
                    LibraryId = b.LibraryId,
                    Title = b.Title,
                    Author = b.Author,
                    ISBN = b.ISBN,
                    TotalCopies = b.TotalCopies,
                    AvailableCopies = b.AvailableCopies,
                    LibraryName = b.Library.LibraryName
                })
                .ToListAsync();
        }
    }
}
