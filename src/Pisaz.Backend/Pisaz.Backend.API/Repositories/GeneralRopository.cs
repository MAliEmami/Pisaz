using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Pisaz.Backend.API.Interfaces;
using Pisaz.Backend.API.Models.Base;
    using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Pisaz.Backend.API.Repositories
{
public class GeneralRepository<TEntity> //: IRepository<TEntity> where TEntity : GeneralModel
{
//     private readonly string _connectionString;

//     public GeneralRepository(string connectionString)
//     {
//         _connectionString = connectionString;
//     }

//     public async Task<int> AddAsync(TEntity entity)
//     {
//         var query = $"INSERT INTO {typeof(TEntity).Name}s (/* List of columns */) VALUES (/* List of values */)";
//         return await ExecuteNonQueryAsync(query);
//     }

//     public async Task<int> UpdateAsync(TEntity entity)
//     {
//         var query = $"UPDATE {typeof(TEntity).Name}s SET /* Column = Value pairs */ WHERE Id = @Id";
//         return await ExecuteNonQueryAsync(query);
//     }

//     public async Task<int> RemoveAsync(TEntity entity)
//     {
//         var query = $"DELETE FROM {typeof(TEntity).Name}s WHERE Id = @Id";
//         return await ExecuteNonQueryAsync(query);
//     }

//     public async Task<IQueryable<TEntity>> GetAllAsync()
//     {
//         var query = $"SELECT * FROM {typeof(TEntity).Name}s";
//         var entities = await ExecuteQueryAsync<TEntity>(query);
//         return entities.AsQueryable();
//     }

//     public async Task<TEntity?> GetByIdAsync(int id)
//     {
//         var query = $"SELECT * FROM {typeof(TEntity).Name}s WHERE Id = @Id";
//         var entities = await ExecuteQueryAsync<TEntity>(query, new SqlParameter("@Id", id));
//         return entities.FirstOrDefault();
//     }

//     private async Task<int> ExecuteNonQueryAsync(string query, params SqlParameter[] parameters)
//     {
//         using (var connection = new SqlConnection(_connectionString))
//         {
//             await connection.OpenAsync();
//             using (var command = new SqlCommand(query, connection))
//             {
//                 command.Parameters.AddRange(parameters);
//                 return await command.ExecuteNonQueryAsync();
//             }
//         }
//     }
}
//     private async Task<List<TEntity>> ExecuteQueryAsync<TEntity>(string query, params SqlParameter[] parameters)
//     {
//         using (var connection = new SqlConnection(_connectionString))
//         {
//             await connection.OpenAsync();
//             using (var command = new SqlCommand(query, connection))
//             {
//                 command.Parameters.AddRange(parameters);
//                 using (var reader = await command.ExecuteReaderAsync())
//                 {
//                     var entities = new List<TEntity>();
//                     while (await reader.ReadAsync())
//                     {
//                         var entity = Activator.CreateInstance<TEntity>();
//                         // Map columns to entity properties
//                         foreach (var prop in typeof(TEntity).GetProperties())
//                         {
//                             if (!reader.IsDBNull(reader.GetOrdinal(prop.Name)))
//                             {
//                                 prop.SetValue(entity, reader[prop.Name]);
//                             }
//                         }
//                         entities.Add(entity);
//                     }
//                     return entities;
//                 }
//             }
//         }
//     }
// }
}



    // public class GeneralRopositor<TEntity> : IRepository<TEntity> where TEntity : GeneralModel
    // {
    //     private readonly IUnitOfWork _unitOfWork;
    //     private readonly DbSet<TEntity> _dbSet;

    //     public GeneralRopositor(IUnitOfWork unitOfWork)
    //     {
    //         _unitOfWork = unitOfWork;
    //         _dbSet = _unitOfWork.GetDbSet<TEntity>();
    //     }
    //     public virtual async Task<int> AddAsync(TEntity entity)
    //     {
    //         await _dbSet.AddAsync(entity);
    //         return await SaveAsync();
    //     }

    //     public virtual async Task<int> UpdateAsync(TEntity entity)
    //     {
    //         _dbSet.Update(entity);
    //         return await SaveAsync();
    //     }

    //     public virtual async Task<int> RemoveAsync(TEntity entity)
    //     {
    //         _dbSet.Remove(entity);
    //         return await SaveAsync();
    //     }

    //     public virtual IQueryable<TEntity> GetAll()
    //     {
    //         return _dbSet;
    //     }

    //     public virtual async Task<TEntity?> GetByIdAsync(int id)
    //     {
    //         return await _dbSet.FirstOrDefaultAsync(m=>m.Id == id);
    //     }

    //     public async Task<int> SaveAsync()
    //     {
    //         return await _unitOfWork.SaveAsync();
    //     }

    // }