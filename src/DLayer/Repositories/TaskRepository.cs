using Core.Enum;
using Core.Interfaces;
using DLayer.Entities;
using DLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace DLayer.Repositories
{
    public class TaskRepository : BaseRepository, ITaskRepository<Task>
    {
        private SqlConnection _connection;
        public TaskRepository() : base()
        {
            _connection = new SqlConnection();
        }
        public void Delete(int id)
        {
            string sp = "spDeleteTask";
            string dbId = "@TaskId";
            ExecuteNonQuery(sp, GetId(dbId,id));
        }

        public void Edit(Task entity)
        {
            string sp = "spUpdateTask";
            var parametersList = AddParameters(entity);
            ExecuteNonQuery(sp, parametersList);
            AddEmployeeTasks(entity);
        }

        public IEnumerable<Task> GetAllWithPaging(int pageNumber)
        {
            string sp = "spGetAllTasksPaging";
            var parametersList = GetParameters(pageNumber);
            return GetEmployees(ExecuteReader<IList<Task>>(sp, parametersList, _connection, listsMapper)); ;
        }

        public Task GetById(int id)
        {
            string sp = "spGetTaskById";
            string dbId = "@TaskId";
            return GetEmployees(ExecuteReader<IList<Task>>(sp, GetId(dbId,id), _connection, listsMapper)).First();
        }

        public int Insert(Task entity)
        {
            string sp = "spAddTask";
            var parametersList = AddParameters(entity);
            int taskId = ExecuteReader<int>(sp, parametersList, _connection, idMapper);
            entity.Id = taskId;
            AddEmployeeTasks(entity);
            return taskId;
        }

        public IEnumerable<Task> GetAllTasksByProjectId(int id)
        {
            string sp = "spGetTasksByProjectId";
            string dbId = "@ProjectId";
            return ExecuteReader<IList<Task>>(sp, GetId(dbId,id), _connection, listsMapper);
        }

        private Func<SqlDataReader, IList<Task>> listsMapper = (sqlDataReader) =>
        {
            List<Task> taskList = new List<Task>();

            while (sqlDataReader.Read())
            {
                Task task = new Task();
                task.Id = Convert.ToInt32(sqlDataReader["Id"]);
                task.Name = sqlDataReader["Name"].ToString();
                task.TaskTime = Convert.ToInt64(sqlDataReader["TaskTime"]);
                task.DateOfStart = Convert.ToDateTime(sqlDataReader["DateOfStart"]);
                task.DateOfEnd = Convert.ToDateTime(sqlDataReader["DateOfEnd"]);
                task.TypeStatus = (Status)Enum.Parse(typeof(Status), sqlDataReader["TypeStatus"].ToString());
                task.ProjectId = Convert.ToInt32(sqlDataReader["ProjectId"]);
                taskList.Add(task);
            }

            return taskList;
        };

        public IEnumerable<Task> GetAll()
        {
            string sp = "spGetAllTasks";
            return ExecuteReader<IList<Task>>(sp, null, _connection, listsMapper);
        }

        private List<SqlParameter> AddParameters(Task entity)
        {
            List<SqlParameter> parametersList = new List<SqlParameter>
            {
                new SqlParameter("@Name", entity.Name),
                new SqlParameter("@TaskTime", entity.TaskTime),
                new SqlParameter("@DateOfStart", entity.DateOfStart),
                new SqlParameter("@DateOfEnd", entity.DateOfEnd),
                new SqlParameter("@Status", entity.TypeStatus),
                new SqlParameter("@ProjectId", entity.ProjectId)
            };

            if (entity.Id == 0)
                return parametersList;
            else
                parametersList.Add(new SqlParameter("@TaskId", entity.Id));
            return parametersList;
        }

        private Func<SqlDataReader, IList<Employee>> EmployeeMapper = (sqlDataReader) =>
        {
            List<Employee> employees = new List<Employee>();

            while (sqlDataReader.Read())
            {
                Employee employee = new Employee();
                employee.Name = sqlDataReader["Name"].ToString();
                employee.Surname = sqlDataReader["Surname"].ToString();
                employee.SecondName = sqlDataReader["SecondName"].ToString();
                employee.Id = Convert.ToInt32(sqlDataReader["Id"]);
                employee.Position = sqlDataReader["Position"].ToString();
                employee.FilePath = sqlDataReader["FilePath"].ToString();
                employees.Add(employee);
            }

            return employees;
        };

        private IEnumerable<Task> GetEmployees(IList<Task> tasks)
        {
            foreach (var task in tasks)
            {
                List<SqlParameter> parameters = new List<SqlParameter>
                {
                    new SqlParameter("@TaskId", task.Id)
                };

                string storedProcedure = "spGetAllEmployeesByTaskId";
                var employees = ExecuteReader<IList<Employee>>(storedProcedure, parameters, _connection, EmployeeMapper);

                foreach (var employee in employees)
                {
                    task.EmployeeTasks.Add(new EmployeeTasks { Employee = employee });
                }
            }

            return tasks;
        }

        private void AddEmployeeTasks(Task entity)
        {
            string storedProcedure = "spAddTasksStaff";

            foreach (var item in entity.EmployeeTasks)
            {
                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("@StaffId", item.EmployeeId));
                parameters.Add(new SqlParameter("@TaskId", entity.Id));
                ExecuteNonQuery(storedProcedure, parameters);
            }

        }
    }
}
