using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Pisaz.Backend.API.Interfaces
{
public interface IUnitOfWork
{
    DbSet<TEntity> GetDbSet<TEntity>() where TEntity : class;
    IRepository<TEntity> GetRepository<TEntity>() where TEntity : class;
    Task<int> SaveAsync();
    IDbConnection Connection { get; } // Add this property
}
}