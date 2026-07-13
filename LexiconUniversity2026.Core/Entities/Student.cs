using System;
using System.Collections.Generic;
using System.Text;

namespace LexiconUniversity2026.Core.Entities
{
    public class Student
    {
        public int Id { get; set; }
        public string Avatar { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string FullName => $"{FirstName} {LastName}"; 

        public string Email { get; set; }

        public Address Address { get; set; }
    }
}
