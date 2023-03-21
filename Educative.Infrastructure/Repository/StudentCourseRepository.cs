using Educative.Core.Entity;
using Educative.Infrastructure.Data.Context;
using Educative.Infrastructure.Interface;

namespace Educative.Infrastructure.Repository
{
    public class StudentCourseRepository : GenericRepository<StudentCourse>, IStudentCourseRepository
    {
        public StudentCourseRepository(EducativeContext context) : base(context)
        {
        }
    }
}
