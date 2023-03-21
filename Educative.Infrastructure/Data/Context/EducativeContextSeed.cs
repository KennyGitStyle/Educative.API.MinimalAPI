using System.Text.Json;
using Educative.Core.Entity;
using Microsoft.Extensions.Logging;

namespace Educative.Infrastructure.Data.Context
{
    public class EducativeContextSeed
    {
        public static async Task SeedDatabaseAsync(EducativeContext context, ILoggerFactory loggerFactory)
        {
            try
            {
                if (!context.Courses.Any())
                {
                    var courses = await File.ReadAllTextAsync("../Educative.Infrastructure/Data/context/DataSeed/Courses.json");

                    var coursesList = JsonSerializer.Deserialize<List<Course>>(courses);

                    if (coursesList != null)
                    {
                        coursesList.ForEach(async c => await context.Courses.AddAsync(c));
                        await context.SaveChangesAsync();
                    }

                }

                if (!context.Students.Any())
                {
                    var students = await File.ReadAllTextAsync("../Educative.Infrastructure/Data/Context/DataSeed/Students.json");

                    var studentsList = JsonSerializer.Deserialize<List<Student>>(students);

                    if (studentsList != null)
                    {
                        studentsList.ForEach(async s => await context.Students.AddAsync(s));
                        await context.SaveChangesAsync();
                    }

                }

                if (!context.Addresses.Any())
                {
                    var addresses = await File.ReadAllTextAsync("../Educative.Infrastructure/Data/Context/DataSeed/Addresses.json");

                    var addressList = JsonSerializer.Deserialize<List<Address>>(addresses);

                    if (addressList != null)
                    {
                        addressList.ForEach(async a => await context.Addresses.AddAsync(a));
                        await context.SaveChangesAsync();
                    }

                }

                if (!context.StudentCourses.Any())
                {
                    var studentCourses = await File.ReadAllTextAsync("../Educative.Infrastructure/Data/Context/DataSeed/StudentCourses.json");

                    var studentCourseList = JsonSerializer.Deserialize<List<StudentCourse>>(studentCourses);
                    if (studentCourseList != null)
                    {
                        studentCourseList.ForEach(async sc => await context.StudentCourses.AddAsync(sc));
                        await context.SaveChangesAsync();
                    }
                }

            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<EducativeContextSeed>();
                logger.LogError("Educative Context Seed Error: ", ex.Message);
            }
        }
    }
}
