namespace Educative.Minimal.API.Dto;

public class StudentCourseDto
{
    public string StudentID { get; set; } = string.Empty!;
    public virtual StudentDto? StudentDto { get; set; }
    public string CourseID { get; set; } = string.Empty!;
    public virtual CourseDto? CourseDto { get; set; }
}