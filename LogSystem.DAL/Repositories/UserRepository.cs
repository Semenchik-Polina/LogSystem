using Dapper;
using LogSystem.DAL.Entities;
using LogSystem.DAL.Interfaces;
using System;
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
            using (IDbConnection connection = new SQLiteConnection(connectionString))
            {
                var user = await connection.QueryAsync<User>(
                    "SELECT * FROM USER WHERE UserID = @userID",
                    param: new { userID = id }
                    //transaction: Transaction
                );
                var result = user.FirstOrDefault();
                return result;
            }
        }

        public async Task<User> GetByUserName(string username)
        {
            using (IDbConnection Connection = new SQLiteConnection(connectionString))
            {
                var result = await Connection.QueryAsync<User>(
                "SELECT * FROM USER WHERE UserName = @username",
                param: new { username }
                //transaction: Transaction
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
                "SELECT seq FROM sqlite_sequence WHERE name = User ",
                param: new
                {
                    firstName = entity.FirstName,
                    lastName = entity.LastName,
                    email = entity.Email,
                    hashedPassword = entity.HashedPassword,
                    type = (int) entity.Type,
                    username = entity.UserName,
                    dynamicSalt = entity.DynamicSalt,
                    registrationDate = entity.RegistrationDate
                }
                //transaction: Transaction
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
                //transaction: Transaction
            );
            }
        }
    }
}
