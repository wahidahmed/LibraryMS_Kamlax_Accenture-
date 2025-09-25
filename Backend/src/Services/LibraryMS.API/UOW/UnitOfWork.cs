using LibraryMS.API.Data;
using LibraryMS.API.Data.Entities;
using LibraryMS.API.Repository;
using LibraryMS.API.Repository.Interfaces;
using LibraryMS.API.UOW.Interfaces;

namespace LibraryMS.API.UOW
{
    public class UnitOfWork: IUnitOfWork
    {
        private AppDbContext _dbContext;
        public UnitOfWork(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IGenericRepository<Book> Book => new GenericRepository<Book>(_dbContext);
        public IGenericRepository<Library> Library => new GenericRepository<Library>(_dbContext);
        public IGenericRepository<Member> Member => new GenericRepository<Member>(_dbContext);
        public IGenericRepository<BookLoan> BookLoan => new GenericRepository<BookLoan>(_dbContext);
        public async Task<bool> SaveAsync()
        {
            return await _dbContext.SaveChangesAsync() > 0;
        }

        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
