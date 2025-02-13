using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pisaz.Backend.API.Interfaces
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetByIdAsync(int id);
        //IQueryable<TEntity> GetAllAsync();
        //Task<TEntity?> GetByIdAsync(int id);
        Task<int> AddAsync(TEntity entity);
        Task<int> UpdateAsync(TEntity entity);
        Task<int> RemoveAsync(TEntity entity); 
        //Task<int> SaveAsync();
    }
}