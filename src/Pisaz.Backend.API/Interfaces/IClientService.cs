using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//using Pisaz.Backend.API.Models.Base;

namespace Pisaz.Backend.API.Interfaces
{
    public interface IClientService<TEntity, TEntityDTO, TAddDTO, TUpdateDTO> where TEntity : class
    {
        Task<IEnumerable<TEntityDTO>> ListAsync(int id);
        Task<TEntity> AddAsync(TAddDTO entity);
        // Task<bool> IsVIP(int id);
        Task<TEntity?> UpdateAsync(int id, TUpdateDTO entity);
    }
}