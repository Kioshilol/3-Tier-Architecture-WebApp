using DLayer.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace DLayer.Repositories
{
    class StaffRep : IRepository<Staff>
    {
        private SqlConnection _connection;
        public StaffRep(SqlConnection connection)
        {
            _connection = connection;
        }
        public void Delete(int id)
        {
            string sp = "spDeleteStaff";
            List<SqlParameter> parametersList = new List<SqlParameter>
            {
                new SqlParameter("@StaffId", id),
            };
            CommonMethods.CommonMethod(sp, parametersList, _connection);
        }

        public void Edit(Staff entity)
        {
            string sp = "spUpdateStaff";
            List<SqlParameter> parametersList = new List<SqlParameter>
            {
                new SqlParameter("@StaffId", entity.Id),
                new SqlParameter("@Name", entity.Name),
                new SqlParameter("@Surname", entity.Surname),
                new SqlParameter("@SecondName", entity.SecondName),
                new SqlParameter("@Position", entity.Position)
            };
            CommonMethods.CommonMethod(sp, parametersList, _connection);
        }

        public IEnumerable<Staff> GetAll()
        {
            List<Staff> staffList = new List<Staff>();
            using (SqlConnection SqlCon = _connection)
            {
                SqlCommand cmd = new SqlCommand("spGetAllStaff", SqlCon);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlCon.Open();
                SqlDataReader sqlDataReader = cmd.ExecuteReader();
                while (sqlDataReader.Read())
                {
                    Staff staff = new Staff();
                    staff.Id = Convert.ToInt32(sqlDataReader["Id"]);
                    staff.Name = sqlDataReader["Name"].ToString();
                    staff.Surname = sqlDataReader["Surname"].ToString();
                    staff.SecondName = sqlDataReader["SecondName"].ToString();
                    staff.Position = sqlDataReader["Position"].ToString();
                    staffList.Add(staff);
                }
                SqlCon.Close();
            }
            return staffList;
        }

        public Staff GetById(int id)
        {
            Staff staff = new Staff();
            using (SqlConnection SqlCon = _connection)
            {
                SqlCommand cmd = new SqlCommand("spGetStaffById", SqlCon);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@StaffId", id);
                SqlCon.Open();
                SqlDataReader sqlDataReader = cmd.ExecuteReader();
                while (sqlDataReader.Read())
                {
                    staff.Id = Convert.ToInt32(sqlDataReader["Id"]);
                    staff.Name = sqlDataReader["Name"].ToString();
                    staff.Surname = sqlDataReader["Surname"].ToString();
                    staff.SecondName = sqlDataReader["SecondName"].ToString();
                    staff.Position = sqlDataReader["Position"].ToString();
                }
                SqlCon.Close();
            }
            return staff;
        }

        public int Insert(Staff entity)
        {
            string sp = "spAddStaff";
            List<SqlParameter> parametersList = new List<SqlParameter>
            {
                new SqlParameter("@Name", entity.Name),
                new SqlParameter("@Surname", entity.Surname),
                new SqlParameter("@SecondName", entity.SecondName),
                new SqlParameter("@Position", entity.Position)
            };
            CommonMethods.CommonMethod(sp, parametersList, _connection);
            return entity.Id;
        }
    }
}
