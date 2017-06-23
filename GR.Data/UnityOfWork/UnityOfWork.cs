using GR.Data.Entities;
using GR.Data.Repository;
using Microsoft.EntityFrameworkCore.Storage;
using System.Threading.Tasks;

namespace GR.Data.UnityOfWork
{
    public class UnityOfWork : IUnityOfWork
    {
        private readonly ApplicationContext _context;

        public UnityOfWork(ApplicationContext context)
        {
            _context = context;
        }

        #region Repositories

        public IRepository<Author> AuthorsRepository => new Repository<Author>(_context);
        public IRepository<Book> BooksRepository => new Repository<Book>(_context);

        #endregion

        public IDbContextTransaction BeginTransactions()
        {
            return _context.Database.BeginTransaction();
        }

        public async Task<IDbContextTransaction> BeginTransactionsAsync()
        {
            return await _context.Database.BeginTransactionAsync();
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
