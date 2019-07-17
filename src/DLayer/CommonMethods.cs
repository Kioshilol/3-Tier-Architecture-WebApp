using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace DLayer
{
    class CommonMethods
    {
        public static void CommonMethod(string storedProcedure, List<SqlParameter> sqlParametersList, SqlConnection connection)
        {
            using (SqlConnection SqlCon = connection)
            {
                SqlCommand cmd = new SqlCommand(storedProcedure, SqlCon);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddRange(sqlParametersList.ToArray());
                SqlCon.Open();
                cmd.ExecuteNonQuery();
                SqlCon.Close();
            }
        }
    }
}
