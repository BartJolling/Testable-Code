using Demo02.LayeredArchitecture.Business;
using Demo02.LayeredArchitecture.Infrastructure;
using Demo02.LayeredArchitecture.Infrastructure.Interfaces;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Demo02.LayeredArchitecture.Facade
{
    public static class ExpenseServiceFactory
    {
        public static ExpenseService Create()
        {
            string connectionString = ConfigurationManager.ConnectionStrings[0].ConnectionString;
            IDbConnection connection = new SqlConnection(connectionString);
            IExpenseRepository repository = new ExpenseRepository(connection);
            return new ExpenseService(repository);
        }
    }
}
