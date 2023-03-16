using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Educative.Core.Entity
{
    public class StudentCourse
    {
        public string StudentID { get; set; } = string.Empty!;
        public virtual Student Student { get; set; } = new();
        public string CourseID { get; set; } = string.Empty!;
        public virtual Course Course { get; set; } = new();
    }
}