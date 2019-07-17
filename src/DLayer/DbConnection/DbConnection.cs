using DLayer.Entities;
using System;
using System.Data.Linq;
using System.Data.SqlClient;

namespace DLayer
{
    public class DbConnection
    {
        public static SqlConnection GetConnection()
        {
            string connectionString = @"Data Source=.\SQLEXPRESS;Database=TrainingTask;Trusted_Connection=True;MultipleActiveResultSets=true";
            SqlConnection connection = new SqlConnection(connectionString);
            return connection;
        }
    }
}
