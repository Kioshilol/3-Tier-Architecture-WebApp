using DLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DLayer
{
    class EFDBContext :DbContext
    {
        DbSet<Project> projects { get; set; }
        DbSet<Staff> Staff { get; set; }
        DbSet<Task> Tasks { get; set; }
        public EFDBContext(DbContextOptions<EFDBContext> options) : base()
        {

        }
    }
}
