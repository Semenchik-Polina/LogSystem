using Dapper;
using LogSystem.DAL.Entities;
using LogSystem.DAL.Interfaces;
using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace LogSystem.DAL.Repositories
{
    internal class UserRepository : RepositoryBase, IUserRepository
    {
        public UserRepository(IDbTransaction transaction)
            :base(transaction)
        { }

        //public async Task<IEnumerable<User>> GetAll()
        //{
        //    var result = await Connection.QueryAsync<User>(
        //        "SELECT * FROM User",
        //        transaction: Transaction
        //        );
        //    return result;   
        //}

        public async Task<User> GetById(int id)
        {
            var user = await Connection.QueryAsync<User>(
                "SELECT * FROM USER WHERE UserID = @userID",
                param: new { userID = id },
                transaction: Transaction
            );
            var result = user.FirstOrDefault();
            return result;
        }

        public async Task<User> GetByUserName(string username)
        {
            var result = await Connection.QueryAsync<User>(
                "SELECT * FROM USER WHERE UserName = @username",
                param: new { username },
                transaction: Transaction
            );
            return result.FirstOrDefault();
        }

        public async Task Insert(User entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("Entity user is null");
            }
            entity.UserID = await Connection.ExecuteScalarAsync<int>(
                "INSERT INTO User (firstName, lastName, email, hashedPassword, type, username,  dynamicSalt, registrationDate) " +
                "VALUES (@firstName, @lastName, @email, @hashedPassword, @type, @username, @dynamicSalt, @registrationDate); " +
                "SELECT  SCOPE_IDENTITY()",
                param: new
                {
                    firstName = entity.FirstName,
                    lastName = entity.LastName,
                    email = entity.Email,
                    hashedPassword = entity.HashedPassword,
                    type = entity.Type,
                    username = entity.UserName,
                    dynamicSalt = entity.DynamicSalt,
                    registrationDate = entity.RegistrationDate
                },
                transaction: Transaction
            );
        }

        public async Task Update(User entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("Entity user is null");
            }
            await Connection.ExecuteAsync(
                "UPDATE User SET firstName = @firstName, lastName = @lastName, email = @email, hashedPassword = @hashedPassword, " +
                "type = @type, username = @username, dynamicSalt = @dynamicSalt, registrationDate = @registrationDate " +
                "WHERE userID = @id",
                param: new
                {
                    firstName = entity.FirstName,
                    lastName = entity.LastName,
                    email = entity.Email,
                    hashedPassword = entity.HashedPassword,
                    type = entity.Type,
                    username = entity.UserName,
                    dynamicSalt = entity.DynamicSalt,
                    registrationDate = entity.RegistrationDate,
                    id = entity.UserID
                },
                transaction: Transaction
            );
        }
    }
}
