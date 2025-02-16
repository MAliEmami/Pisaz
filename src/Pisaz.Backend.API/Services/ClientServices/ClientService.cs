using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Pisaz.Backend.API.DTOs.Clients;
using Pisaz.Backend.API.DTOs.Clients.SignIn;
using Pisaz.Backend.API.Interfaces;
using Pisaz.Backend.API.Models.ClientModels;
using Microsoft.EntityFrameworkCore;
using Pisaz.Backend.API.DTOs.ClientsDTOs.Dashboard;
using Microsoft.Data.SqlClient;
using Pisaz.Backend.API.DbContextes;


namespace Pisaz.Backend.API.Services.ClientServices
{
    public class ClientService(IRepository<Client> clients, IRepository<Address> address, PisazDB db) 
    : IService<Client, ClientDTO, ClientAddDTO, ClientUpdateDTO>
    {
        private readonly PisazDB _db = db;
        private readonly IRepository<Client> _clients = clients;
        private readonly IRepository<Address> _address = address;

        public async Task<IEnumerable<ClientDTO>> ListAsync(int id)
        {
            const string ClientInfoQuery = @"
                SELECT 
                    FirstName,     
                    LastName,     
                    PhoneNumber,   
                    WalletBalance, 
                    ReferralCode, 
                    SignupDate, 
                    Province,
                    Remainder
                FROM Client C 
                JOIN Address A ON C.ID = A.ID
                WHERE C.ID = @id";


            var clientInfoList = await _db.Database
                                        .SqlQueryRaw<ClientDTO>(ClientInfoQuery, new SqlParameter("@id", id))
                                        .ToListAsync();

            return clientInfoList
            .Select(c => new ClientDTO
            {
                FirstName = c.FirstName,
                LastName = c.LastName,
                PhoneNumber = c.PhoneNumber,
                WalletBalance = c.WalletBalance,
                ReferralCode = c.ReferralCode,
                SignupDate = c.SignupDate,
                Province = c.Province,
                Remainder = c.Remainder
            }).ToList();
        }


            // const string sql = @"
            //     SELECT 
            //         C.FirstName, 
            //         C.LastName, 
            //         C.PhoneNumber, 
            //         C.WalletBalance, 
            //         C.ReferralCode, 
            //         C.SignupDate, 
            //         A.Province, 
            //         A.Remainder 
            //     FROM Client C JOIN Address A ON C.ID = A.ID
            //     WHERE C.ID = @id";

            // var clients = new List<ClientDTO>();

            // using (var connection = _db.Database.GetDbConnection())
            // {
            //     await connection.OpenAsync();
            //     using var command = connection.CreateCommand();

            //     command.CommandText = sql;
            //     command.Parameters.Add(new SqlParameter("@id", id));

            //     using var reader = await command.ExecuteReaderAsync();
            //     while (await reader.ReadAsync())
            //     {
            //         clients.Add(new ClientDTO
            //         {
            //             FirstName = reader["FirstName"].ToString()!,
            //             LastName = reader["LastName"].ToString()!,
            //             PhoneNumber = reader["PhoneNumber"].ToString()!,
            //             WalletBalance = Convert.ToDecimal(reader["WalletBalance"]),
            //             ReferralCode = reader["ReferralCode"] as string,
            //             SignupDate = Convert.ToDateTime(reader["SignupDate"]),
            //             Address = new AddressDTO
            //             {
            //                 Province = reader["Province"].ToString()!,
            //                 Remainder = reader["Remainder"].ToString()!
            //             },
            //         });
            //     }

            //     command.CommandText = sql;
            //     command.Parameters.Add(new SqlParameter("@id", 2));
            //     //using var reader = await command.ExecuteReaderAsync();
            //     while (await reader.ReadAsync())
            //     {
            //         clients.Add(new ClientDTO
            //         {
            //             FirstName = reader["FirstName"].ToString()!,
            //             LastName = reader["LastName"].ToString()!,
            //             PhoneNumber = reader["PhoneNumber"].ToString()!,
            //             WalletBalance = Convert.ToDecimal(reader["WalletBalance"]),
            //             ReferralCode = reader["ReferralCode"] as string,
            //             SignupDate = Convert.ToDateTime(reader["SignupDate"]),
            //             Address = new AddressDTO
            //             {
            //                 Province = reader["Province"].ToString()!,
            //                 Remainder = reader["Remainder"].ToString()!
            //             },
            //         });
            //     }
            // }

            // return clients;


        // public async Task<IEnumerable<ClientDTO>> ListAsync(int id)
        // {
        //     const string sql = @"
        //         SELECT 
        //             C.ID AS ClientID, 
        //             C.FirstName, 
        //             C.LastName, 
        //             C.PhoneNumber, 
        //             C.WalletBalance, 
        //             C.ReferralCode, 
        //             C.SignupDate, 
        //             A.ID AS AddressID, 
        //             A.Province, 
        //             A.Remainder 
        //         FROM Client C 
        //         JOIN Address A ON C.ID = A.ClientID 
        //         WHERE C.ID = @id";

        //     var clients = await _clients
        //         .FromSqlRaw(sql, new SqlParameter("@id", id))
        //         .Select(c => new ClientDTO
        //         {
        //             FirstName = c.FirstName,
        //             LastName = c.LastName,
        //             PhoneNumber = c.PhoneNumber,
        //             WalletBalance = c.WalletBalance,
        //             ReferralCode = c.ReferralCode,
        //             SignupDate = c.SignupDate,
        //             Address = new AddressDTO
        //             {
        //                 Province = c.Address.Province,
        //                 Remainder = c.Address.Remainder
        //             }
        //         })
        //         .ToListAsync();

        //     return clients;
        // }

        // public async Task<IEnumerable<ClientDTO>> ListAsync(int id)
        // {
        //     const string sql = "SELECT * FROM Client C JOIN Address A ON C.ID = A.ID";
            
        //     return await _clients
        //         .GetByIdAsync(id)
        //         .FromSqlRaw(sql, id)
        //         .Select(c => new ClientDTO
        //         {
        //             FirstName = c.FirstName,
        //             LastName = c.LastName,
        //             PhoneNumber = c.PhoneNumber,
        //             WalletBalance = c.WalletBalance,
        //             ReferralCode = c.ReferralCode,
        //             SignupDate = c.SignupDate,

        //             Address = new AddressDTO
        //             {
        //                 ID = c.Address.ID,
        //                 Province = c.Address.Province,
        //                 Remainder = c.Address.Remainder
        //             }
        //         })
        //         .ToList();
        // }
        public async Task<int> AddAsync(ClientAddDTO entity)
        {
            var c = new Client
            {
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                PhoneNumber = entity.PhoneNumber,
                WalletBalance = default,
                ReferralCode = default,
                SignupDate = DateTime.Now
            };
            return await _clients.AddAsync(c);
        }

        public async Task<int> UpdateAsync(int id, ClientUpdateDTO entity)
         {
        //     // var dbClient = await _clients.GetByIdAsync(id);
        //     // if (dbClient != null)
        //     // {
        //     //     dbClient.FirstName = entity.FirstName;
        //     //     dbClient.LastName = entity.LastName;
        //     //     dbClient.PhoneNumber = entity.PhoneNumber;
        //     //     return await _clients.UpdateAsync(dbClient);
        //     // }
             return 0;
         }
    }
}