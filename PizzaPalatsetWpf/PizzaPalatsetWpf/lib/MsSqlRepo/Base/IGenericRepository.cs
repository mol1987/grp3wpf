using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MsSqlRepo
{
    public interface IGenericRepository<T>
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task DeleteRowAsync(int id);
        Task<T> GetAsync(int id);
        Task<int> SaveRangeAsync(IEnumerable<T> list);
        Task UpdateAsync(T t);
        Task InsertAsync(T t);
        Task DryInsert(T t);
    }
}
