using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vibbraapi.Domain.Entities;
using vibbraapi.Infra.Mappings;

namespace vibbraapi.Infra.Contexts
{
    public class VibbraContext:DbContext
    {
        public VibbraContext(DbContextOptions<VibbraContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }

        public DbSet<Project> Projects { get; set; }

        public DbSet<Time> Times { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserMap());
            modelBuilder.ApplyConfiguration(new ProjectMap());
            modelBuilder.ApplyConfiguration(new TimeMap());
            base.OnModelCreating(modelBuilder);
        }
    }
}
