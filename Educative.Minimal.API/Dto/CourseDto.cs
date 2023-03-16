
namespace Educative.Minimal.API.Dto;
public class CourseDto
{
    public string CourseID { get; set; } = string.Empty!;
    public string CourseName { get; set; } = string.Empty!;
    public string CourseTutor { get; set; } = string.Empty!;
    public string CourseDescription { get; set; } = string.Empty!;
    public string CourseTopic { get; set; } = string.Empty!;
    public decimal Price { get; set; }
    public virtual ICollection<StudentCourseDto> StudentCourseDtos { get; set; } = new List<StudentCourseDto>();
}