using Dapper;
using LogSystem.DAL.Entities;
using LogSystem.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Threading.Tasks;

namespace LogSystem.DAL.Repositories
{
    public class UserActionRepository: RepositoryBase, IUserActionRepository
    {
        public UserActionRepository()
            : base()
        { }
        
        // get all userActions
        public async Task<IEnumerable<UserAction>> GetAll()
        {
            using (IDbConnection connection = new SQLiteConnection(connectionString))
            {
                var userActions = await connection.QueryAsync<UserAction, User, UserAction>(
                "SELECT * FROM UserAction ua " +
                "LEFT JOIN User u " +
                "ON ua.FK_UserID = u.UserID ",
                (userAction, user) =>
                {
                    userAction.User = user;
                    return userAction;
                },
                buffered: true,
                splitOn: "FK_UserID"
                );
                return userActions;
            }
        }

        // get all userActions filtered by date
        // store date as string because SQLite doesn't have DateTime type
        public async Task<IEnumerable<UserAction>> GetByDate(string date)
        {
            using (IDbConnection connection = new SQLiteConnection(connectionString))
            {
                var result = await connection.QueryAsync<UserAction, User, UserAction>(
                "SELECT * FROM UserAction ua " +
                "LEFT JOIN User u " +
                "ON ua.FK_UserID = u.UserID " +
                "WHERE ua.Date = @date",
                 (userAction, user) =>
                 {
                     userAction.User = user;
                     return userAction;
                 },
                param: new { date },
                buffered: true,
                splitOn: "FK_UserID"
                );
                return result;
            }
        }

        public async Task Insert(UserAction entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("Entity user is null");
            }
            using (IDbConnection connection = new SQLiteConnection(connectionString))
            {

                await connection.ExecuteAsync(
                "INSERT INTO UserAction (FK_UserID, Date, Type) " +
                "VALUES (@FK_UserID, @Date, @type) ",
                param: new
                {
                    entity.FK_UserID,
                    entity.Date,
                    type = (int) entity.Type
                }
            );
            }
        }
    }
}
