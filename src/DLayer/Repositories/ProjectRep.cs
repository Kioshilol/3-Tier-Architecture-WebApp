using DLayer.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DLayer.Repositories
{
    public class ProjectRep : IRepository<Project>
    {
        private SqlConnection _connection;
        public ProjectRep(SqlConnection connection)
        {
            _connection = connection;
        }
        
        public void Delete(int id)
        {
            string sp = "spDeleteProject";
            List<SqlParameter> parametersList = new List<SqlParameter>
            {
                new SqlParameter("@ProjectId", id),
            };
            CommonMethods.CommonMethod(sp,parametersList,_connection);
        }
        public void Edit(Project entity)
        {
            string sp = "spUpdateProject";
            List<SqlParameter> parametersList = new List<SqlParameter>
            {
                new SqlParameter("@ProjectId", entity.Id),
                new SqlParameter("@Name", entity.Name),
                new SqlParameter("@ShortName", entity.ShortName),
                new SqlParameter("@Description", entity.Description)
            };
            CommonMethods.CommonMethod(sp, parametersList, _connection);
        }

        public IEnumerable<Project> GetAll()
        {
            List<Project> projectList = new List<Project>();
            using (SqlConnection SqlCon = _connection)
            {
                SqlCommand cmd = new SqlCommand("spGetAllProjects", SqlCon);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlCon.Open();
                SqlDataReader sqlDataReader = cmd.ExecuteReader();
                while(sqlDataReader.Read())
                {
                    Project project = new Project();
                    project.Id = Convert.ToInt32(sqlDataReader["Id"]);
                    project.Name = sqlDataReader["Name"].ToString();
                    project.ShortName = sqlDataReader["ShortName"].ToString();
                    project.Description = sqlDataReader["Description"].ToString();
                    projectList.Add(project);
                }
                SqlCon.Close();
            }
            return projectList;
        }

        public Project GetById(int id)
        {
            Project project = new Project();
            using (SqlConnection SqlCon = _connection)
            {
                SqlCommand cmd = new SqlCommand("spGetProjectById", SqlCon);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ProjectId", id);
                SqlCon.Open();
                SqlDataReader sqlDataReader = cmd.ExecuteReader();
                while(sqlDataReader.Read())
                {
                    project.Id = Convert.ToInt32(sqlDataReader["Id"]);
                    project.Name = sqlDataReader["Name"].ToString();
                    project.ShortName = sqlDataReader["ShortName"].ToString();
                    project.Description = sqlDataReader["Description"].ToString();
                }
                SqlCon.Close();
            }
            return project;
        }

        public int Insert(Project entity)
        {
            string sp = "spAddProject";
            List<SqlParameter> parametersList = new List<SqlParameter>
            {
                new SqlParameter("@Name", entity.Name),
                new SqlParameter("@ShortName", entity.ShortName),
                new SqlParameter("@Description", entity.Description)
            };
            CommonMethods.CommonMethod(sp, parametersList, _connection);
            return entity.Id;
        }
    }
}
