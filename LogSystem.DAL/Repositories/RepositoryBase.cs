using System.Configuration;

namespace LogSystem.DAL.Repositories
{
    public abstract class RepositoryBase
    {
        protected readonly string connectionString;

        public RepositoryBase()
        {
            connectionString = ConfigurationManager.ConnectionStrings["default"].ToString();
        }
    }
}
