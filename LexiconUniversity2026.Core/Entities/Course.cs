using System;
using System.Collections.Generic;
using System.Text;

namespace LexiconUniversity2026.Core.Entities
{
    public class Course
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;

        public ICollection<Enrollment> Enrollments { get; set; }

        public ICollection<Student> Students { get; set; }
    }
}
