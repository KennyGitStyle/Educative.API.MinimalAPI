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
                if(!context.Students.Any())
                {
                    var students = await File.ReadAllTextAsync("../Educative.Infrastructure/Data/Context/DataSeed/Students.json");

                    var studentList = JsonSerializer.Deserialize<List<Student>>(students);
                    if (studentList != null)
                    {
                        studentList.ForEach(async s => await context.Students.AddAsync(s!));
                        await context.SaveChangesAsync();
                    }

                    await context.SaveChangesAsync();
                }

                if(!context.Courses.Any())
                {
                    var courses = await File.ReadAllTextAsync("../Educative.Infrastructure/Data/Context/DataSeed/Courses.json");
                    
                    var coursesList = JsonSerializer.Deserialize<List<Course>>(courses);

                    if (coursesList != null)
                    {
                        coursesList.ForEach(async c => await context.Courses.AddAsync(c));
                        await context.SaveChangesAsync();
                    }

                    await context.SaveChangesAsync();
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
