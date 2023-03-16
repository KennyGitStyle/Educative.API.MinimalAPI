using System.ComponentModel.DataAnnotations;

namespace Educative.Core.Entity
{
    public class Course
    {
        [Key]
        public string CourseID { get; set; } = string.Empty!; 
        [Display(Name = "Course Name")]
        public string CourseName { get; set; } = string.Empty!;
        [Display(Name = "Course Tutor")]
        public string CourseTutor { get; set; } = string.Empty!;
        [Display(Name = "Course Description")]
        public string CourseDescription { get; set; } = string.Empty!;
        [StringLength(60)]
        public string CourseTopic { get; set; } = string.Empty!;
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }
        public virtual ICollection<StudentCourse> StudentCourses { get; set; } = new List<StudentCourse>();
    }
}