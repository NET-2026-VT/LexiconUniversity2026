using LexiconUniversity2026.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Text;

namespace LexiconUniversity2026.Persistence.Configurations
{
    public class StudentConfigurations : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> modelBuilder)
        {
            //Wrong name in db
            //modelBuilder.Entity<Student>().OwnsOne(s => s.Name); 

            modelBuilder
                .OwnsOne(s => s.Name)
                .Property(n => n.FirstName)
                .HasColumnName("FirstName");
            modelBuilder
                .OwnsOne(s => s.Name)
                .Property(n => n.LastName)
                .HasColumnName("LastName");

            modelBuilder.Property<DateTime>("Edited");

            modelBuilder
                .HasMany(s => s.Courses)
                .WithMany(c => c.Students)
                .UsingEntity<Enrollment>(
                e => e.HasOne(e => e.Course).WithMany(c => c.Enrollments),
                e => e.HasOne(e => e.Student).WithMany(s => s.Enrollments),
                e => e.HasKey(e => new { e.CourseId, e.StudentId }));
        }
    }
}
