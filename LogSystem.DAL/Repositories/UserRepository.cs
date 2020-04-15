using Dapper;
using LogSystem.Common.Enums;
using LogSystem.DAL.Entities;
using LogSystem.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Threading.Tasks;

namespace LogSystem.DAL.Repositories
{
    public class UserRepository : RepositoryBase, IUserRepository
    {
        public UserRepository()
            :base()
        { }
        
        // get all users filtered by UserType
        public async Task<IEnumerable<User>> GetAllByUserType(UserType type)
        {
            using (IDbConnection Connection = new SQLiteConnection(connectionString))
            {
                var result = await Connection.QueryAsync<User>(
                "SELECT * FROM USER WHERE Type = @type",
                param: new { type = (int)type }
            );
                return result;
            }
        }

        // get user by id
        public async Task<User> GetById(int id)
        {
            using (IDbConnection connection = new SQLiteConnection(connectionString))
            {
                var user = await connection.QueryAsync<User>(
                    "SELECT * FROM USER WHERE UserID = @userID",
                    param: new { userID = id }
                );
                var result = user.FirstOrDefault();
                return result;
            }
        }

        // get first ot default user by username
        public async Task<User> GetByUserName(string username)
        {
            using (IDbConnection Connection = new SQLiteConnection(connectionString))
            {
                var result = await Connection.QueryAsync<User>(
                "SELECT * FROM USER WHERE UserName = @username",
                param: new { username }
            );
                return result.FirstOrDefault();
            }
        }
        
        public async Task<int> Insert(User entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("Entity user is null");
            }
            using (IDbConnection Connection = new SQLiteConnection(connectionString))
            {
                entity.UserID = await Connection.ExecuteScalarAsync<int>(
                "INSERT INTO User (firstName, lastName, email, hashedPassword, type, username,  dynamicSalt, registrationDate) " +
                "VALUES (@firstName, @lastName, @email, @hashedPassword, @type, @username, @dynamicSalt, @registrationDate); " +
                "SELECT last_insert_rowid(); ",
                param: new
                {
                    firstName = entity.FirstName,
                    lastName = entity.LastName,
                    email = entity.Email,
                    hashedPassword = entity.HashedPassword,
                    type = (int)entity.Type,
                    username = entity.UserName,
                    dynamicSalt = entity.DynamicSalt,
                    registrationDate = entity.RegistrationDate
                }
            );
            }
            return entity.UserID;
        }

        public async Task Update(User entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("Entity user is null");
            }
            using (IDbConnection Connection = new SQLiteConnection(connectionString))
            {
                await Connection.ExecuteAsync(
                "UPDATE User SET firstName = @firstName, lastName = @lastName, email = @email, hashedPassword = @hashedPassword, " +
                "type = @type " +
                "WHERE userID = @id",
                param: new
                {
                    firstName = entity.FirstName,
                    lastName = entity.LastName,
                    email = entity.Email,
                    hashedPassword = entity.HashedPassword,
                    type = (int) entity.Type,
                    id = entity.UserID
                }
            );
            }
        }
    }
}
