


using Microsoft.EntityFrameworkCore.Storage;

namespace courseProject.Core.IGenericRepository
{
    public interface IGenericRepository1<T> where T : class
    {


        public Task<int> saveAsync();
        Task<IReadOnlyList<T>> GetAllEmployeeAsync();            
     //   Task<T> GetEmployeeById(Guid id);
        public Task updateEmployeeAsync(T entity);
        public Task<IDbContextTransaction> BeginTransactionAsync();
       



    }
}
