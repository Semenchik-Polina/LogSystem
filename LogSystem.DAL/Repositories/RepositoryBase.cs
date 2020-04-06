using System.Configuration;
using System.Data;

namespace LogSystem.DAL.Repositories
{
    public abstract class RepositoryBase
    {
        protected readonly string connectionString;

        //protected IDbTransaction Transaction { get; private set; }
        //protected IDbConnection Connection { get { return Transaction.Connection; } }

        public RepositoryBase()
        {
            connectionString = ConfigurationManager.ConnectionStrings["default"].ToString();
        }
    }
}
