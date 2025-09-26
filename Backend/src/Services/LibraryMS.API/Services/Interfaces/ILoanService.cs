using LibraryMS.API.Dtos;

namespace LibraryMS.API.Services.Interfaces
{
    public interface ILoanService
    {
        Task<IEnumerable<LoanResponseDto>> GetActiveLoansAsync();
        Task<string> BorrowBookAsync(BorrowRequestDto request);
        Task<bool> ReturnBookAsync(int loanId);
        Task<IEnumerable<LoanResponseDto>> GetOverdueLoansAsync();
    }
}
