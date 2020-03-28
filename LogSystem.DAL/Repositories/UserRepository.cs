using Dapper;
using LogSystem.DAL.Entities;
using LogSystem.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogSystem.DAL.Repositories
{
    internal class UserRepository : RepositoryBase, IUserRepository
    {
        public UserRepository(IDbTransaction transaction)
            :base(transaction)
        { }

        public async Task<IEnumerable<User>> GetAll()
        {
            var result = await Connection.QueryAsync<User>(
                "SELECT * FROM User",
                transaction: Transaction
                );
            return result;   
        }

    }
}
