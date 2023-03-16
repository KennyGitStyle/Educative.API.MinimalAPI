using AutoMapper;
using Educative.Core.Entity;
using Educative.Minimal.API.Dto;

namespace Educative.Minimal.API.Config;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Address, AddressDto>().ReverseMap();
        CreateMap<Student, StudentDto>().ReverseMap();
        CreateMap<Course, CourseDto>().ReverseMap();
        CreateMap<StudentCourse, StudentCourseDto>();
    }
}