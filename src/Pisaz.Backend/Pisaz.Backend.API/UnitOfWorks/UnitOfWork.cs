using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Pisaz.Backend.API.DbContextes;
using Pisaz.Backend.API.Interfaces;

namespace Pisaz.Backend.API.UnitOfWorks
{
    public class UnitOfWork(PisazDB db, IServiceProvider serviceProvider) : IUnitOfWork
{
    private readonly PisazDB _db = db;
    private readonly IServiceProvider _serviceProvider = serviceProvider;

    public DbSet<TEntity> GetDbSet<TEntity>() where TEntity : class
    {
        return _db.Set<TEntity>();
    }

    public IRepository<TEntity> GetRepository<TEntity>() where TEntity : class
    {
        var results = _serviceProvider.GetServices(typeof(IRepository<TEntity>)).FirstOrDefault();
        if (results != null)
        {
            return (IRepository<TEntity>)results;
        }
        throw new Exception("Unknown Service");
    }

    public async Task<int> SaveAsync()
    {
        return await _db.SaveChangesAsync();
    }

    // Implement the Connection property
    public IDbConnection Connection => _db.Database.GetDbConnection();
}
}
    // public class UnitOfWork(PisazDB db, IServiceProvider serviceProvider) : IUnitOfWork
    // {
    //     private readonly PisazDB _db = db;
    //     private readonly IServiceProvider _serviceProvider = serviceProvider;

    //     public DbSet<TEntity> GetDbSet<TEntity>() where TEntity : class
    //     {
    //         return _db.Set<TEntity>();
    //     }

    //     public IRepository<TEntity> GetRepository<TEntity>() where TEntity : class
    //     {
    //         var results = _serviceProvider.GetServices(typeof(IRepository<TEntity>)).FirstOrDefault();
    //         if (results != null)
    //         {
    //             return (IRepository<TEntity>)results;
    //         }
    //         throw new Exception("Unknown Service");
    //     }

    //     public async Task<int> SaveAsync()
    //     {
    //         return await _db.SaveChangesAsync();
    //     }
    // }