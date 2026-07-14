using System;
using System.Collections.Generic;
using System.Text;

namespace LexiconUniversity2026.Core.Entities
{
    public class Student
    {
        public int Id { get; set; }
        public string Avatar { get; set; } = string.Empty;

        public Name Name { get; set; } = new Name(); 

        public string Email { get; set; } = string.Empty;

        public Address Address { get; set; } = new Address();

        public ICollection<Enrollment> Enrollments { get; set; }
        public ICollection<Course> Courses { get; set; }
    }

    public class Name
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;

        public string FullName => $"{FirstName} {LastName}";
    }
}
