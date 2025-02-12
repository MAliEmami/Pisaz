using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Pisaz.Backend.API.Interfaces;
using Pisaz.Backend.API.Models.Base;

namespace Pisaz.Backend.API.Repositories
{
    public class GeneralRopositor<TEntity> : IRepository<TEntity> where TEntity : GeneralModel
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly DbSet<TEntity> _dbSet;

        public GeneralRopositor(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _dbSet = _unitOfWork.GetDbSet<TEntity>();
        }
        public virtual async Task<int> AddAsync(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
            return await SaveAsync();
        }

        public virtual async Task<int> UpdateAsync(TEntity entity)
        {
            _dbSet.Update(entity);
            return await SaveAsync();
        }

        public virtual async Task<int> RemoveAsync(TEntity entity)
        {
            _dbSet.Remove(entity);
            return await SaveAsync();
        }

        public virtual IQueryable<TEntity> GetAll()
        {
            return _dbSet;
        }

        public virtual async Task<TEntity?> GetByIdAsync(int id)
        {
            return await _dbSet.FirstOrDefaultAsync(m=>m.Id == id);
        }

        public async Task<int> SaveAsync()
        {
            return await _unitOfWork.SaveAsync();
        }

    }
}