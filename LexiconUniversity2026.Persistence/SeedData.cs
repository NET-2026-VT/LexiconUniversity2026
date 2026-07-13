using Bogus;
using LexiconUniversity2026.Core.Entities;
using LexiconUniversity2026.Persistence.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace LexiconUniversity2026.Persistence
{
    public static class SeedData
    {
        private static Faker _faker;

        public static async Task InitAsync(LexiconUniversityContext context)
        {
            if (await context.Students.AnyAsync()) return;

            _faker = new Faker("sv");

            IEnumerable<Student> students = GenerateStudents(50);
            await context.AddRangeAsync(students);

            IEnumerable<Course> courses = GenerateCourses(20);
            await context.AddRangeAsync(courses);

            IEnumerable<Enrollment> enrollments = GenerateEnrollments(students, courses);
            await context.AddRangeAsync(enrollments);

            await context.SaveChangesAsync(); 

        }

        private static IEnumerable<Enrollment> GenerateEnrollments(IEnumerable<Student> students, IEnumerable<Course> courses)
        {
            Random rnd = new Random();

            List<Enrollment> enrollments = new List<Enrollment>();

            foreach (Student student in students)
            {
                foreach (Course course in courses)
                {
                    if(rnd.Next(0,5) == 0)
                    {
                        Enrollment enrollment = new Enrollment
                        {
                            Course = course,
                            Student = student,
                            Grade = rnd.Next(1, 5)
                        };
                        enrollments.Add(enrollment); 
                    }
                }
            }
            return enrollments; 
        }

        private static IEnumerable<Course> GenerateCourses(int numberOfCourses)
        {
            List<Course> courses = new List<Course>();

            for (int i = 0; i < numberOfCourses; i++)
            {
                Course course = new Course
                {
                    Title = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(_faker.Company.Bs())
                };
                courses.Add(course);
            }

            return courses;
        }

        private static IEnumerable<Student> GenerateStudents(int numberOfStudents)
        {
            List<Student> students = new List<Student>();

            for (int i = 0; i < numberOfStudents; i++)
            {
                string fName = _faker.Name.FirstName();
                string lName = _faker.Name.LastName();
                Student student = new Student()
                {
                    Avatar = _faker.Internet.Avatar(),
                    FirstName = fName,
                    LastName = lName,
                    Email = _faker.Internet.Email(fName, lName, "lexicon.se"),
                    Address = new Address
                    {
                        Street = _faker.Address.StreetAddress(),
                        ZipCode = _faker.Address.ZipCode(),
                        City = _faker.Address.City()
                    }
                };
                students.Add(student);
            }

            return students;
        }
    }
}
