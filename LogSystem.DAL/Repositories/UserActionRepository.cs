using Dapper;
using LogSystem.Common.Enums;
using LogSystem.DAL.Entities;
using LogSystem.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace LogSystem.DAL.Repositories
{
    internal class UserActionRepository: RepositoryBase, IUserActionRepository
    {
        public UserActionRepository(IDbTransaction transaction)
            : base(transaction)
        { }

        /// <summary>
        /// get all userActions
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<UserAction>> GetAll()
        {
            var result = await Connection.QueryAsync<UserAction>(
                "SELECT * FROM UserAction",
                transaction: Transaction
                );
            return result;
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
            var result = await Connection.QueryAsync<UserAction>(
                "SELECT * FROM UserAction WHERE Date = @date",
                // store date as string because SQLite doesn't have DateTime type
                param: new { date = date.ToString() },
                transaction: Transaction
            );
            return result;
        }

        /// <summary>
        /// get all userActions filtered by UserActionType(enum)
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public async Task<IEnumerable<UserAction>> GetByType(UserActionType type)
        {
            var result = await Connection.QueryAsync<UserAction>(
                "SELECT * FROM UserAction WHERE Type = @type",
                param: new { type = (int)type },
                transaction: Transaction
            );
            return result;
        }

        /// <summary>
        /// get all userActions filtered by UserId
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public async Task<IEnumerable<UserAction>> GetByUserID(int userID)
        {
            var result = await Connection.QueryAsync<UserAction>(
                "SELECT * FROM UserAction WHERE UserID = @userID",
                param: new { userID },
                transaction: Transaction
            );
            return result;
        }

        public async Task Insert(UserAction entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("Entity user is null");
            }
            entity.UserActionID = await Connection.ExecuteScalarAsync<int>(
                "INSERT INTO UserAction (userID, date, type) " +
                "VALUES (@userID, @date, @type); " +
                "SELECT  SCOPE_IDENTITY()",
                param: new
                {
                    userID = entity.UserID,
                    date = entity.Date,
                    type = entity.Type
                },
                transaction: Transaction
            );
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
