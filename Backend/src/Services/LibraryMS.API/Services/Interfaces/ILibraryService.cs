using LibraryMS.API.Dtos;

namespace LibraryMS.API.Services.Interfaces
{
    public interface ILibraryService
    {
        Task<LibraryStatsDto> GetStats(int id);
    }
}
