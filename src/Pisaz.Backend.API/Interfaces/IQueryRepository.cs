using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pisaz.Backend.API.Interfaces
{
    public interface IQueryRepository<TEntity> where TEntity : class
    {
        Task<List<TEntity?>> GetByIdAsync(int id);
    }
}