using System.Linq.Expressions;
using Educative.Core.Entity;

namespace Educative.Infrastructure.Interface
{
    public interface IStudentRepository : IGenericRepository<Student> 
    {
        Task<IEnumerable<Student>> FilterAttendanceAsync(Expression<Func<Student, bool>> predicate);
    }
}
