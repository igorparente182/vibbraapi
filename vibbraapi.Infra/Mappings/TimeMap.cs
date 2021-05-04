using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vibbraapi.Domain.Entities;

namespace vibbraapi.Infra.Mappings
{
    public class TimeMap : IEntityTypeConfiguration<Time>
    {
        public void Configure(EntityTypeBuilder<Time> builder)
        {
            builder.ToTable("TB_TIME");
            builder.Property(t => t.Id).HasColumnName("time_id");

            builder.HasKey(x => new { x.Id, x.Project_Id, x.User_Id });

            builder.HasOne(t => t.project)
                .WithMany(t => t.Times)
                .HasForeignKey(t => t.Project_Id);

            builder.HasOne(t => t.user)
                .WithMany(t => t.Times)
                .HasForeignKey(t => t.User_Id);
        }
    }
}
