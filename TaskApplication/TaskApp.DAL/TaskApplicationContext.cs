using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TaskApp.Model;

namespace TaskApp.DAL
{
    public class TaskApplicationContext: DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=Task;Trusted_Connection=True;");
        }
        public TaskApplicationContext() { }
        public TaskApplicationContext(DbContextOptions<TaskApplicationContext> options) : base(options) { 

        }
        public DbSet<TaskModel> Tasks { get; set; }
        public DbSet<UserModel> Users { get; set; }
        public DbSet<CategoryModel> Categories { get; set; }

    }
}
