namespace Educative.Core.Entity
{
    public class StudentCourse
    {
        public string StudentID { get; set; } = string.Empty!;
        public virtual Student? Student { get; set; }
        public string CourseID { get; set; } = string.Empty!;
        public virtual Course? Course { get; set; }
        
    }
}