using GR.Data.Entities;
using GR.Data.Repository;
using Microsoft.EntityFrameworkCore.Storage;
using System.Threading.Tasks;

namespace GR.Data.UnityOfWork
{
    public interface IUnityOfWork
    {
        #region Repositories 

        IRepository<Author> AuthorsRepository { get; }
        IRepository<Book> BooksRepository { get; }

        #endregion
        
        IDbContextTransaction BeginTransactions();

        Task<IDbContextTransaction> BeginTransactionsAsync();

        void SaveChanges();
    }
}
