namespace Educative.Infrastructure.Helpers
{
    public class CourseParams : PaginationParams
    {
        public string OrderBy { get; set; } = string.Empty!;
        public string SearchTerm { get; set; } = string.Empty!;
        public string Name { get; set; } = string.Empty!; 
        public string Topic { get; set; } = string.Empty!; 
    }
}