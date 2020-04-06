using Dapper;
using LogSystem.Common.Enums;
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

        /// <summary>
        /// get all userActions
        /// </summary>
        /// <returns></returns>
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

        //public async Task<UserAction> GetById(int id)
        //{
        //    var userAction = await Connection.QueryAsync<UserAction>(
        //        "SELECT * FROM USER WHERE UserActionID = @userActionID",
        //        param: new { userActionID = id },
        //        transaction: Transaction
        //    );
        //    var result = userAction.FirstOrDefault();
        //    return result;
        //}

        /// <summary>
        /// get all userActions filtered by date
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public async Task<IEnumerable<UserAction>> GetByDate(DateTime date)
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

        /// <summary>
        /// get all userActions filtered by UserActionType(enum)
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public async Task<IEnumerable<UserAction>> GetByType(UserActionType type)
        {
            using (IDbConnection connection = new SQLiteConnection(connectionString))
            {
                var result = await connection.QueryAsync<UserAction, User, UserAction>(
                "SELECT * FROM UserAction ua " +
                "LEFT JOIN User u " +
                "ON ua.FK_UserID = u.UserID " +
                "WHERE ua.Type = @type",
                 (userAction, user) =>
                 {
                     userAction.User = user;
                     return userAction;
                 }, 
                param: new { type = (int) type },
                buffered: true,
                splitOn: "FK_UserID"
                );
                return result;
            }
        }

        /// <summary>
        /// get all userActions filtered by UserId
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public async Task<IEnumerable<UserAction>> GetByUserID(int FK_UserID)
        {
            using (IDbConnection connection = new SQLiteConnection(connectionString))
            {
                var result = await connection.QueryAsync<UserAction, User, UserAction>(
                "SELECT * FROM UserAction ua" +
                "LEFT JOIN User u " +
                "ON ua.FK_UserID = u.UserID " +
                "WHERE ua.FK_UserID = @FK_UserID",
                (userAction, user) =>
                {
                    userAction.User = user;
                    return userAction;
                },
                param: new { FK_UserID },
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

        //public async Task Update(UserAction entity)
        //{
        //    if (entity == null)
        //    {
        //        throw new ArgumentNullException("Entity user is null");
        //    }
        //    await Connection.ExecuteAsync(
        //        "UPDATE UserAction SET userID = @userID, date = @date, type = @type " +
        //        "WHERE userActionID = @id",
        //        param: new
        //        {
        //            userID = entity.UserID,
        //            date = entity.Date,
        //            type = entity.Type,
        //            id = entity.UserActionID
        //        },
        //        transaction: Transaction
        //    );
        //}

    }
}
