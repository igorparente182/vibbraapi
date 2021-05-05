using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vibbraapi.Domain.Entities;

namespace vibbraapi.Infra.Mappings
{
    public class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("TB_USER");
            builder.Property(u => u.Id).HasColumnName("user_id").ValueGeneratedOnAdd();
            builder.Property(u => u.Password).HasColumnName("senha");

        }
    }
}
