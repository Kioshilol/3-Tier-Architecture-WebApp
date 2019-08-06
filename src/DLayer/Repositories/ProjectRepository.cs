using Core.Enum;
using Core.Interfaces;
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
    public class ProjectRepository : BaseRepository, IRepository<Project>
    {
        private SqlConnection _connection;

        public ProjectRepository() : base()
        {
            _connection = new SqlConnection();
        }

        public void Delete(int id)
        {
            string sp = "spDeleteProject";
            string projectId = "@ProjectId";
            ExecuteNonQuery(sp, GetId(projectId, id));
        }
        public void Edit(Project entity)
        {
            string sp = "spUpdateProject";
            var parametersList = AddParameters(entity);
            ExecuteNonQuery(sp, parametersList);
        }

        public IEnumerable<Project> GetAllWithPaging(int pageNumber)
        {
            string sp = "spGetAllProjectsPaging";
            var parametersList = GetParameters(pageNumber);
            return ExecuteReader<IList<Project>>(sp, parametersList, _connection, listsMapper);
        }

        public IEnumerable<Project> GetAll()
        {
            string sp = "spGetAllProjects";
            return ExecuteReader<IList<Project>>(sp, null, _connection, listsMapper);
        }

        public Project GetById(int id)
        {
            string sp = "spGetProjectById";
            string projectId = "@ProjectId";
            return ExecuteReader<IList<Project>>(sp, GetId(projectId, id), _connection, listsMapper).First();
        }

        public int Insert(Project entity)
        {
            string sp = "spAddProject";
            var parametersList = AddParameters(entity);
            return ExecuteReader<int>(sp, parametersList, _connection, idMapper);
        }

        private Func<SqlDataReader, IList<Project>> listsMapper = (sqlDataReader) =>
        {
            List<Project> projectList = new List<Project>();

            while (sqlDataReader.Read())
            {
                Project project = new Project();
                project.Id = Convert.ToInt32(sqlDataReader["Id"]);
                project.Name = sqlDataReader["Name"].ToString();
                project.ShortName = sqlDataReader["ShortName"].ToString();
                project.Description = sqlDataReader["Description"].ToString();
                projectList.Add(project);
            }

            return projectList;
        };

        private List<SqlParameter> AddParameters(Project entity)
        {
            List<SqlParameter> parametersList = new List<SqlParameter>
            {
                new SqlParameter("@Name", entity.Name),
                new SqlParameter("@ShortName", entity.ShortName),
                new SqlParameter("@Description", entity.Description)
            };

            if (entity.Id == null)
                return parametersList;
            else
                parametersList.Add(new SqlParameter("@ProjectId", entity.Id));
            return parametersList;
        }
    }
}
