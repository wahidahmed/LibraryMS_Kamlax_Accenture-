using LibraryMS.API.Data.Entities;
using LibraryMS.API.Repository.Interfaces;

namespace LibraryMS.API.UOW.Interfaces
{
    public interface IUnitOfWork
    {
        IGenericRepository<Book> Book { get; }
        IGenericRepository<Library> Library { get; }
        IGenericRepository<BookLoan> BookLoan { get; }
        IGenericRepository<Member> Member { get; }
    }
}
