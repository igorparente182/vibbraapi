using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vibbraapi.Domain.Entities;

namespace vibbraapi.Infra.Contexts
{
    public class VibbraContext:DbContext
    {
        public VibbraContext(DbContextOptions<VibbraContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .ToTable("TB_USER");
            modelBuilder.Entity<User>().HasIndex(b => b.Id);
            modelBuilder.Entity<User>().Property(u => u.Id).HasColumnName("user_id");
            modelBuilder.Entity<User>().Property(u => u.Password).HasColumnName("senha");
        }
    }
}
