using System.ComponentModel.DataAnnotations;

namespace Educative.Core.Entity
{
    public class Student
    {
        [Key]
        public string StudentID { get; set; } = string.Empty!;
        [Display(Name = "Firstname")]
        public string Firstname { get; set; } = string.Empty!;
        [Display(Name = "Middlename")]
        public char? MiddlenameInitial { get; set; } 
        [Display(Name = "Lastname")]
        public string Lastname { get; set; } = string.Empty!;
        public DateTime? DateOfBirth { get; set; }
        public virtual Address Address { get; set; } = new();
        [DataType(DataType.PhoneNumber)]
        public string PhoneNo { get; set; } = string.Empty!;
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; } = string.Empty!;
        public int Attendance { get; set; }
        public virtual ICollection<StudentCourse> StudentCourses { get; set; } = new List<StudentCourse>();
        
    }
}