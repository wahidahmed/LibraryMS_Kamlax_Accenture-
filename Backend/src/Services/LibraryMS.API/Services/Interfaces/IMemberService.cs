using LibraryMS.API.Dtos;

namespace LibraryMS.API.Services.Interfaces
{
    public interface IMemberService
    {
        Task<MemberDto> GetById(int id);
        Task<IEnumerable<MemberDto>> Get();
    }
}
