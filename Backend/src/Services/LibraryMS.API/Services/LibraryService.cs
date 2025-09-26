using LibraryMS.API.Data;
using LibraryMS.API.Dtos;
using LibraryMS.API.Helpers;
using LibraryMS.API.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LibraryMS.API.Services
{
    public class LibraryService: ILibraryService
    {
        private readonly AppDbContext _context;

        public LibraryService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<LibraryStatsDto> GetStats(int id)
        {
            var library = await _context.Libraries.FindAsync(id);
            //if (library == null)
            //    return NotFound();

            var totalBooks = await _context.Books.CountAsync(b => b.LibraryId == id);
            var availableCopies = await _context.Books
                .Where(b => b.LibraryId == id)
                .SumAsync(b => b.AvailableCopies);

            var totalMembers = await _context.Members.CountAsync(m => m.LibraryId == id);

            var overdueCount = await _context.BookLoans
                .Where(l => l.LibraryId == id &&
                            l.DueDate < DateTime.UtcNow.ToDateOnly() &&
                            l.ReturnDate == null)
                .CountAsync();

            var activeLoans = await _context.BookLoans
                .CountAsync(l => l.LibraryId == id && l.ReturnDate == null);

            var stats = new LibraryStatsDto
            {
                LibraryId = id,
                Name = library.LibraryName,
                TotalBooks = totalBooks,
                AvailableCopies = availableCopies,
                TotalMembers = totalMembers,
                ActiveLoans = activeLoans,
                OverdueLoans = overdueCount
            };

            return stats;
        }
    }
}
