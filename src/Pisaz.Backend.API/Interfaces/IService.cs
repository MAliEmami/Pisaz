using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//using Pisaz.Backend.API.Models.Base;

namespace Pisaz.Backend.API.Interfaces
{
    public interface IService<TEntity, TEntityDTO, TAddDTO, TUpdateDTO> where TEntity : class// GeneralModel
    {
        Task<IEnumerable<TEntityDTO>> ListAsync(int id);
        Task<int> AddAsync(TAddDTO entity);
        Task<int> UpdateAsync(int id, TUpdateDTO entity);
        // Task<int> RemoveAsync(int id);
    }
}