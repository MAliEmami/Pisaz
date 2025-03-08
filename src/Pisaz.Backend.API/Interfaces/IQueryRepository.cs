using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pisaz.Backend.API.Interfaces
{
    public interface IQueryRepository<T> where T : class
    {
        Task<List<T?>> GetByIdAsync(int id);
    }
}