using DLayer.Entities;
using DLayer.Interfaces;
using DLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace DLayer
{
    public class UnitOfWork : IUnitOfWork
    {
        private SqlConnection _connection;
        private ProjectRep projectRep;
        private StaffRep staffRep;
        private TaskRep taskRep;
        private TaskRep taskByIdRep;
        private TaskRep taskInsertStaff;
        string connectionString = @"Data Source=.\SQLEXPRESS;Database=TrainingTask;Trusted_Connection=True;MultipleActiveResultSets=true";
        public UnitOfWork()
        {
            _connection = new SqlConnection(connectionString);
        }

        public IRepository<Project> Projects
        {
            get
            {
                if (projectRep == null)
                    projectRep = new ProjectRep(_connection);
                return projectRep;
            }
        }

        public IRepository<Staff> Staff
        {
            get
            {
                if (staffRep == null)
                    staffRep = new StaffRep(_connection);
                return staffRep;
            }
        }

        public IRepository<Task> Task
        {
            get
            {
                if (taskByIdRep == null)
                    taskByIdRep = new TaskRep(_connection);
                return taskByIdRep;
            }
        }
        public IGetAllById<Task> TasksById
        {
            get
            {
                if (taskRep == null)
                    taskRep = new TaskRep(_connection);
                return taskRep;
            }
        }

        public IInsert<Task> InsertStaff
        {
            get
            {
                if (taskInsertStaff == null)
                    taskInsertStaff = new TaskRep(_connection);
                return taskInsertStaff;
            }
        }

        public void Dispose()
        {

        }

        public void Save()
        {

        }
    }
}

