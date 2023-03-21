namespace Educative.Minimal.API.Dto;

public class StudentDto
{
    public string StudentID { get; set; } = string.Empty!;
    public string Firstname { get; set; } = string.Empty!;
    public char? MiddlenameInitial { get; set; } 
    public string Lastname { get; set; } = string.Empty!;
    public DateTime? DateOfBirth { get; set; }
    public AddressDto? AddressDto { get; set; }
    public string PhoneNo { get; set; } = string.Empty!;
    public string Email { get; set; } = string.Empty!;
    public int Attendance { get; set; }
    public ICollection<StudentCourseDto> StudentCourseDtos { get; set; } = new List<StudentCourseDto>();
}