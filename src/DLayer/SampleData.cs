using DLayer.Entities;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace DLayer
{
    public class SampleData
    {
        public static void Data()
        {

            Project p1 = new Project("Hello world", "HW", "Some description");
            string connectionString = @"Data Source=.\SQLEXPRESS;Database=TrainingTask;Trusted_Connection=True;MultipleActiveResultSets=true";
;
            string sqlExpression = String.Format("INSERT INTO Project (Name, ShortName,Description) VALUES ('{0}', '{1}', '{2}')", p1.Name, p1.ShortName, p1.Description);
            //DataContext db = new DataContext(connectionString);

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                if (connection.State == System.Data.ConnectionState.Closed)
                {
                    connection.Open();
                    SqlCommand sqlCommand = new SqlCommand(sqlExpression, connection);
                    sqlCommand.ExecuteNonQuery();
                }
            }
        }
    }
}
