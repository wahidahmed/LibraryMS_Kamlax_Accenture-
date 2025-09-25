using LibraryMS.API.Data;
using LibraryMS.API.Data.Entities;
using LibraryMS.API.Dtos;
using LibraryMS.API.Helpers;
using LibraryMS.API.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;

namespace LibraryMS.API.Services
{
    public class LoanService: ILoanService
    {
        private readonly AppDbContext _context;

        public LoanService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> BorrowBookAsync(BorrowRequestDto request)
        {
            var book = await _context.Books.FindAsync(request.BookId);
            var member = await _context.Members.FindAsync(request.MemberId);
            var library = await _context.Libraries.FindAsync(request.LibraryId);

            if (book == null || member == null || library == null)
                return false;

            if (book.AvailableCopies <= 0)
                return false;

            if (book.LibraryId != request.LibraryId || member.LibraryId != request.LibraryId)
                return false; // Must belong to same library

            var loan = new BookLoan
            {
                BookId = request.BookId,
                MemberId = request.MemberId,
                LibraryId = request.LibraryId,
                LoanDate = DateTime.UtcNow.ToDateOnly(),
                DueDate = DateTime.UtcNow.ToDateOnly().AddDays(14) //borrow for two week loan
            };

            book.AvailableCopies--;

            _context.BookLoans.Add(loan);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> ReturnBookAsync(int loanId)
        {
            var loan = await _context.BookLoans.FindAsync(loanId);
            if (loan == null || loan.ReturnDate != null)
                return false;

            loan.ReturnDate = DateTime.UtcNow.ToDateOnly();

            var book = await _context.Books.FindAsync(loan.BookId);
            if (book != null)
                book.AvailableCopies++;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<LoanResponseDto>> GetActiveLoansAsync()
        {
            var today = DateTime.UtcNow.ToDateOnly();// ToDateOnly is an Extension  method for Dateonly convertion

            var overdueLoans = _context.BookLoans
                .Where(l => l.DueDate < today && l.ReturnDate == null)
                .Include(l => l.Book)
                .Include(l => l.Member)
                .Select(l => new LoanResponseDto
                {
                    BookLoanId = l.BookLoanId,
                    BookTitle = l.Book.Title,
                    MemberName = l.Member.Name,
                    LoanDate = l.LoanDate,
                    DueDate = l.DueDate,
                    ReturnDate = l.ReturnDate,
                    IsOverdue = true
                })
                .AsQueryable();

            return await overdueLoans.ToListAsync();
            
        }

        public async Task<IEnumerable<LoanResponseDto>> GetOverdueLoansAsync()
        {
            var result = _context.BookLoans
                .Where(l => l.DueDate < DateTime.UtcNow.ToDateOnly() && l.ReturnDate == null)
                .Include(l => l.Book)
                .Include(l => l.Member)
                .Select(l => new LoanResponseDto
                {
                    BookLoanId = l.BookLoanId,
                    BookTitle = l.Book.Title,
                    MemberName = l.Member.Name,
                    LoanDate = l.LoanDate,
                    DueDate = l.DueDate,
                    ReturnDate = l.ReturnDate,
                    IsOverdue = true
                }).AsQueryable();
                return await result.ToListAsync();
        }

    }
}
