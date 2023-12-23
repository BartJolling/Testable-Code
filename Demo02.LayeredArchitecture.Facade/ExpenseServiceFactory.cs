using Demo02.LayeredArchitecture.Business;
using Demo02.LayeredArchitecture.Infrastructure;
using Demo02.LayeredArchitecture.Infrastructure.Interfaces;
using System;
using System.Configuration;
using System.Data;
using Microsoft.Data.SqlClient;

namespace Demo02.LayeredArchitecture.Facade
{
    public static class ExpenseServiceFactory
    {
        public static ExpenseService Create()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ExpensesDatabase"].ConnectionString;
            IDbConnection connection = new SqlConnection(connectionString);
            IExpenseRepository repository = new ExpenseRepository(connection);
            
            return ExpenseServiceFactory.Create(repository);
        }

        public static ExpenseService Create(IExpenseRepository repository)
        {
            if (repository == null)
                throw new ArgumentNullException("Expense Repository");

            return new ExpenseService(repository);
        }
    }
}
