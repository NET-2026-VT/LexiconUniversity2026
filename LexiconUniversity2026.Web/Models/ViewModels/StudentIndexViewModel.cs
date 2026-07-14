namespace LexiconUniversity2026.Web.Models.ViewModels
{
    public class StudentIndexViewModel
    {
        public int Id { get; set; }
        public string Avatar { get; set; }
        public string FullName { get; set; }

        public string City { get; set; }

        public IEnumerable<CourseInfo> CourseInfos { get; set; }
    }

    public class CourseInfo
    {
        public string CourseName { get; set; }
        public int Grade { get; set; }
    }
}
