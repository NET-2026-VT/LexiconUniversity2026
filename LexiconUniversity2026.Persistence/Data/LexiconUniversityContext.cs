using LexiconUniversity2026.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace LexiconUniversity2026.Persistence.Data
{
    public class LexiconUniversityContext : DbContext
    {
        public LexiconUniversityContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Student> Students { get; set; } = default!; 
        public DbSet<Address> Addresses { get; set; } = default!;

        public DbSet<Course> Courses { get; set; } = default!;

        public DbSet<Enrollment> Enrollments { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Wrong name in db
            //modelBuilder.Entity<Student>().OwnsOne(s => s.Name); 

            modelBuilder.Entity<Student>()
                .OwnsOne(s => s.Name)
                .Property(n => n.FirstName)
                .HasColumnName("FirstName");
            modelBuilder.Entity<Student>()
                .OwnsOne(s => s.Name)
                .Property(n => n.LastName)
                .HasColumnName("LastName");

            modelBuilder.Entity<Student>()
                .HasMany(s => s.Courses)
                .WithMany(c => c.Students)
                .UsingEntity<Enrollment>(
                e => e.HasOne(e => e.Course).WithMany(c => c.Enrollments),
                e => e.HasOne(e => e.Student).WithMany(s => s.Enrollments),
                e => e.HasKey(e => new { e.CourseId, e.StudentId }));

            //modelBuilder.Entity<Enrollment>().HasKey(e => new { e.CourseId, e.StudentId }); 
        }
    }
}
