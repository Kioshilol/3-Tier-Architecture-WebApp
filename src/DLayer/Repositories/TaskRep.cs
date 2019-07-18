using DLayer.Entities;
using DLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace DLayer.Repositories
{
    public class TaskRep : IRepository<Task>, IGetAllById<Task>
    {
        private SqlConnection _connection;
        public TaskRep(SqlConnection connection)
        {
            _connection = connection;
        }
        public void Delete(int id)
        {
            string sp = "spDeleteTask";
            List<SqlParameter> parametersList = new List<SqlParameter>
            {
                new SqlParameter("@TaskId", id),
            };
            CommonMethods.CommonMethod(sp, parametersList, _connection);
        }

        public void Edit(Task entity)
        {
            string sp = "spUpdateTask";
            List<SqlParameter> parametersList = new List<SqlParameter>
            {
                new SqlParameter("@TaskId", entity.Id),
                new SqlParameter("@Name", entity.Name),
                new SqlParameter("@TaskTime", entity.TaskTime),
                new SqlParameter("@DateOfStart", entity.DateOfStart),
                new SqlParameter("@DateOfEnd", entity.DateOfEnd),
                new SqlParameter("@Status", entity.TypeStatus)
            };
            CommonMethods.CommonMethod(sp, parametersList, _connection);
        }

        public IEnumerable<Task> GetAll()
        {
            List<Task> taskList = new List<Task>();
            using (SqlConnection SqlCon = _connection)
            {
                SqlCommand cmd = new SqlCommand("spGetAllTasks", SqlCon);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlCon.Open();
                SqlDataReader sqlDataReader = cmd.ExecuteReader();
                while (sqlDataReader.Read())
                {
                    Task task = new Task();
                    task.Id = Convert.ToInt32(sqlDataReader["Id"]);
                    task.Name = sqlDataReader["Name"].ToString();
                    task.TaskTime = Convert.ToInt64(sqlDataReader["TaskTime"]);
                    task.DateOfStart = Convert.ToDateTime(sqlDataReader["DateOfStart"]);
                    task.DateOfEnd = Convert.ToDateTime(sqlDataReader["DateOfEnd"]);
                    task.TypeStatus = (Task.EnumTypeOfStatus)Enum.Parse(typeof(Task.EnumTypeOfStatus), sqlDataReader["TypeStatus"].ToString());
                    taskList.Add(task);
                }
                SqlCon.Close();
            }
            return taskList;
        }

        public Task GetById(int id)
        {
            Task task = new Task();
            using (SqlConnection SqlCon = _connection)
            {
                SqlCommand cmd = new SqlCommand("spGetTaskById", SqlCon);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@TaskId", id);
                SqlCon.Open();
                SqlDataReader sqlDataReader = cmd.ExecuteReader();
                while (sqlDataReader.Read())
                {
                    task.Id = Convert.ToInt32(sqlDataReader["Id"]);
                    task.Name = sqlDataReader["Name"].ToString();
                    task.TaskTime = Convert.ToInt64(sqlDataReader["TaskTime"]);
                    task.DateOfStart = Convert.ToDateTime(sqlDataReader["DateOfStart"]);
                    task.DateOfEnd = Convert.ToDateTime(sqlDataReader["DateOfEnd"]);
                    task.TypeStatus = (Task.EnumTypeOfStatus)Enum.Parse(typeof(Task.EnumTypeOfStatus), sqlDataReader["TypeStatus"].ToString());
                }
                SqlCon.Close();
            }
            return task;
        }

        public void Insert(Task entity)
        {
            string sp = "spAddTask";
            List<SqlParameter> parametersList = new List<SqlParameter>
            {

                new SqlParameter("@Name", entity.Name),
                new SqlParameter("@TaskTime", entity.TaskTime),
                new SqlParameter("@DateOfStart", entity.DateOfStart),
                new SqlParameter("@DateOfEnd", entity.DateOfEnd),
                new SqlParameter("@Status", entity.TypeStatus)
            };
            CommonMethods.CommonMethod(sp, parametersList, _connection);
        }

        public IEnumerable<Task> GettAllById(int id)
        {
            List<Task> tasks = new List<Task>();
            using (SqlConnection SqlCon = _connection)
            {
                SqlCommand cmd = new SqlCommand("spGetTasksByProjectId", SqlCon);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ProjectId", id);
                SqlCon.Open();
                SqlDataReader sqlDataReader = cmd.ExecuteReader();
                while (sqlDataReader.Read())
                {
                    Task task = new Task();
                    task.Id = Convert.ToInt32(sqlDataReader["Id"]);
                    task.Name = sqlDataReader["Name"].ToString();
                    task.TaskTime = Convert.ToInt64(sqlDataReader["TaskTime"]);
                    task.DateOfStart = Convert.ToDateTime(sqlDataReader["DateOfStart"]);
                    task.DateOfEnd = Convert.ToDateTime(sqlDataReader["DateOfEnd"]);
                    task.TypeStatus = (Task.EnumTypeOfStatus)Enum.Parse(typeof(Task.EnumTypeOfStatus), sqlDataReader["TypeStatus"].ToString());
                    tasks.Add(task);
                }
                SqlCon.Close();
            }
            return tasks;
        }
    }
}
