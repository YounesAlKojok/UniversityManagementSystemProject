namespace UniversityManagementSystem.Models
{
    public partial class Department
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public int DeanId { get; set; }

        public int FacultyId { get; set; }

        public virtual Dean? Dean { get; set; } = null!;

        public virtual Faculty? Faculty { get; set; } = null!;

        public virtual ICollection<Student> Students { get; set; } = new List<Student>();

        public virtual ICollection<Course> Courses { get; set; } = new List<Course>();


        //addon methods and new var

        //public void AddCourse(Course course) { Courses.Add(course); }
        //public void RemoveCourse(Course course) { Courses.Remove(course); }
        //public int GetStudentCount() { return Students.Count; }
        //public int GetCourseCount() { return Courses.Count; }
    }
}
