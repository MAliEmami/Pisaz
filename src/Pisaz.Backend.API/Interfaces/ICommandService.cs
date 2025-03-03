using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pisaz.Backend.API.Interfaces
{
    public interface ICommandService<TEntity, TAddDTO, TUpdateDTO> where TEntity : class
    {
        Task<TEntity> AddAsync(TAddDTO entity);
        Task<TEntity?> UpdateAsync(int id, TUpdateDTO entity);
    }
}