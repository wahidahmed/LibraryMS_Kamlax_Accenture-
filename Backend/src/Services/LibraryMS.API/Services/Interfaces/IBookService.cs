using LibraryMS.API.Dtos;

namespace LibraryMS.API.Services.Interfaces
{
    public interface IBookService
    {
        Task<BookDto> GetByIdAsync(int id);
        Task<IEnumerable<BookDto>> GetAllAsync();
        Task<IEnumerable<BookDto>> SearchBooksAsync(string query, int? libraryId = null);
    }
}
