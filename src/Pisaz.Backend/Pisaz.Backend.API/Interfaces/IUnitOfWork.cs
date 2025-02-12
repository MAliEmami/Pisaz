using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Pisaz.Backend.API.Interfaces
{
    public interface IUnitOfWork
    {
        IRepository<TEntity> GetRepository<TEntity>() where TEntity : class;
        DbSet<TEntity> GetDbSet<TEntity>() where TEntity : class;
        Task<int> SaveAsync();
    }
}