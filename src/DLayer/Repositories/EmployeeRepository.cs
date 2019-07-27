using Core.Interfaces;
using DLayer.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace DLayer.Repositories
{
    public class EmployeeRepository :BaseRepository, IRepository<Employee>
    {
        private SqlConnection _connection;
        public EmployeeRepository() : base()
        {
            _connection = new SqlConnection();
        }
        public void Delete(int id)
        {
            string sp = "spDeleteStaff";
            string dbId = "@StaffId";
            ExecuteNonQuery(sp, GetId(dbId,id));
        }

        public void Edit(Employee entity)
        {
            string sp = "spUpdateStaff";
            var parametersList = AddParameters(entity);
            ExecuteNonQuery(sp, parametersList);
        }

        public IEnumerable<Employee> GetAllWithPaging(int pageNumber)
        {
            string sp = "spGetAllStaffPaging";
            var parametersList = GetParameters(pageNumber);
            return ExecuteReader<IList<Employee>>(sp, parametersList, _connection, listsMapper);
        }

        public Employee GetById(int id)
        {
            string sp = "spGetStaffById";
            string dbId = "@StaffId";
            return ExecuteReader<IList<Employee>>(sp, GetId(dbId, id), _connection, listsMapper).First();
        }

        public int Insert(Employee entity)
        {
            string sp = "spAddStaff";
            var parametersList = AddParameters(entity);
            return ExecuteReader<int>(sp, parametersList, _connection, idMapper);
        }

        public IEnumerable<Employee> GetAll()
        {
            string sp = "spGetAllStaff";
            return ExecuteReader<IList<Employee>>(sp, null, _connection, listsMapper);
        }

        private Func<SqlDataReader, IList<Employee>> listsMapper = (sqlDataReader) =>
        {
            List<Employee> staffList = new List<Employee>();

            while (sqlDataReader.Read())
            {
                Employee staff = new Employee();
                staff.Id = Convert.ToInt32(sqlDataReader["Id"]);
                staff.Name = sqlDataReader["Name"].ToString();
                staff.Surname = sqlDataReader["Surname"].ToString();
                staff.SecondName = sqlDataReader["SecondName"].ToString();
                staff.Position = sqlDataReader["Position"].ToString();
                staffList.Add(staff);
            }

            return staffList;
        };

        private List<SqlParameter> AddParameters(Employee entity)
        {
            List<SqlParameter> parametersList = new List<SqlParameter>
            {
                new SqlParameter("@Name", entity.Name),
                new SqlParameter("@Surname", entity.Surname),
                new SqlParameter("@SecondName", entity.SecondName),
                new SqlParameter("@Position", entity.Position)
            };

            if (entity.Id == null)
                return parametersList;
            else
                parametersList.Add(new SqlParameter("@StaffId", entity.Id));
            return parametersList;
        }
    }
}
