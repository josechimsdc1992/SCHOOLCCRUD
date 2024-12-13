using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Mapping
{
    public class MapGrade : IEntityTypeConfiguration<Grade>
    {
        public void Configure(EntityTypeBuilder<Grade> builder)
        {
            builder.ToTable("Grade");
            builder.HasKey(e => e.IdGrade);

            builder
          .HasOne(e => e.Teacher)
          .WithMany(x => x.Grades)
          .HasForeignKey(z => z.IdGrades);
        }
    }
}
