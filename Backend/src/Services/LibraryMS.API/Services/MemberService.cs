using LibraryMS.API.Data;
using LibraryMS.API.Dtos;
using LibraryMS.API.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LibraryMS.API.Services
{
    public class MemberService: IMemberService
    {
        private readonly AppDbContext _context;

        public MemberService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<MemberDto>> Get()
        {
            var result = _context.Members
               .Include(l => l.Library)
               .Select(l => new MemberDto
               {
                   MemberId = l.MemberId,
                   Name = l.Name,
                   Email = l.Email,
                   Phone = l.Phone,
                   Address = l.Address,
                   MembershipDate = l.MembershipDate,
                   LibraryId = l.LibraryId,
                   LibraryName = l.Library.LibraryName
               })
               .AsQueryable();

            return await result.ToListAsync();
        }

        public async Task<MemberDto> GetById(int id)
        {
            var result = _context.Members
               .Where(l => l.MemberId==id)
               .Include(l => l.Library)
               .Select(l => new MemberDto
               {
                   MemberId = l.MemberId,
                   Name = l.Name,
                   Email = l.Email,
                   Phone = l.Phone,
                   Address = l.Address,
                   MembershipDate = l.MembershipDate,
                   LibraryId = l.LibraryId,
                   LibraryName=l.Library.LibraryName
               })
               .AsQueryable();

            return await result.FirstOrDefaultAsync();
        }
    }
}
