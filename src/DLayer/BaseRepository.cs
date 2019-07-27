using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace DLayer
{

    public abstract class BaseRepository
    {
        private SqlConnection _connection;
        public string ConnectionString = AppSetting.GetConnectionString();
        public BaseRepository()
        {
            _connection = new SqlConnection(AppSetting.GetConnectionString());
        }
        protected void ExecuteNonQuery(string storedProcedure, List<SqlParameter> sqlParametersList)
        {
            SqlCommand cmd;
            using (SqlConnection SqlCon = new SqlConnection(ConnectionString))
            {
                cmd = new SqlCommand(storedProcedure, SqlCon);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddRange(sqlParametersList.ToArray());
                SqlCon.Open();
                cmd.ExecuteNonQuery();
                SqlCon.Close();
            }
        }

        protected T ExecuteReader<T>(string storedProcedure, List<SqlParameter> sqlParametersList, SqlConnection connection, Func<SqlDataReader, T> mapper)
        {
            T result;
            using (SqlConnection SqlCon = new SqlConnection(ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(storedProcedure, SqlCon);
                cmd.CommandType = CommandType.StoredProcedure;

                if (sqlParametersList != null)
                {
                    cmd.Parameters.AddRange(sqlParametersList.ToArray());
                }

                SqlCon.Open();
                SqlDataReader sqlDataReader = cmd.ExecuteReader();
                result = mapper(sqlDataReader);
                SqlCon.Close();
            }
            return result;
        }

        protected Func<SqlDataReader, int> idMapper = (sqlDataReader) =>
        {
            int id = -1;

            if (sqlDataReader.Read())
            {
                id = Convert.ToInt32(sqlDataReader["Id"]);
            }

            return id;
        };

        protected List<SqlParameter> GetParameters(int pageNumber)
        {
            int pageSize = AppSetting.GetPageSize();

            List<SqlParameter> parametersList = new List<SqlParameter>
            {
                new SqlParameter("@RecordsPerPage", pageSize),
                new SqlParameter("@PageNumber", pageNumber)
            };

            return parametersList;
        }

        protected List<SqlParameter> GetId(string dbId, int id)
        {
            List<SqlParameter> parametersList = new List<SqlParameter>
            {
                new SqlParameter(dbId, id),
            };
            return parametersList;
        }
    }
}
