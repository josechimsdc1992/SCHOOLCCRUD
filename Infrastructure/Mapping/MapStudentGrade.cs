﻿using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Mapping
{
    public class MapStudentGrade : IEntityTypeConfiguration<StudentGrade>
    {
        public void Configure(EntityTypeBuilder<StudentGrade> builder)
        {
            builder.ToTable("StudentGrade");
            builder.HasKey(e => e.IdStudentGrade);

            builder
         .HasOne(e => e.Student)
         .WithMany(x => x.StudentGrades)
         .HasForeignKey(z => z.IdStudent);

            builder
        .HasOne(e => e.Grade)
        .WithMany(x => x.StudentGrades)
        .HasForeignKey(z => z.IdGrade);


        }
    }
}
