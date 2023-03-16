using Educative.Core.Entity;
using Educative.Infrastructure.Interface;
using Educative.Minimal.API.Dto;
using AutoMapper;
using Educative.Infrastructure.Helpers;
using Educative.Infrastructure.Repository;
using Educative.Minimal.API.Config;
using Educative.Minimal.API.Extension;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAutoMapper(typeof(MappingProfiles));
builder.Services.AddScoped<ICourseRepository, CourseRepository>();
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContextExtension(builder.Configuration);
builder.Services.AddCorsService();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

await app.UseDbInitializer();

app.MapGet("/courses", async (ICourseRepository courseRepo, [AsParameters] CourseParams courseParams, IMapper mapper) =>
{
    var courses = await courseRepo.GetAllCourses(courseParams);
    var result = mapper.Map<IEnumerable<CourseDto>>(courses);

    return Results.Ok(courses);

}).Produces<IEnumerable<CourseDto>>();

app.MapGet("/courses/{id}", async (ICourseRepository courseRepo, IMapper mapper, string id) =>
{
    var course = await courseRepo.GetByIDAsync(id);

    if (course == null)
    {
        return Results.NotFound();
    }

    var result = mapper.Map<CourseDto>(course);

    return Results.Ok(result);
}).Produces<CourseDto>();

app.MapPost("/courses", async (ICourseRepository courseRepo, IMapper mapper, CourseDto courseDto) =>
{
    if (courseDto == null)
    {
        return Results.BadRequest();
    }

    var course = mapper.Map<Course>(courseDto);
    await courseRepo.AddAsync(course);

    var createdCourse = mapper.Map<CourseDto>(course);

    return Results.CreatedAtRoute("GetCourseByID", new { id = createdCourse.CourseID.ToLower() }, createdCourse);
}).Produces<CourseDto>();

app.MapPut("/courses/{id}", async (ICourseRepository courseRepo, IMapper mapper, string id, CourseDto courseDto) =>
{
    if (courseDto == null || string.IsNullOrWhiteSpace(id))
    {
        return Results.BadRequest();
    }

    var existing = await courseRepo.GetByIDAsync(id);

    if (existing == null)
    {
        return Results.NotFound();
    }

    mapper.Map(courseDto, existing);
    await courseRepo.UpdateAsync(existing);

    return Results.NoContent();
});

app.MapDelete("/courses/{id}", async (ICourseRepository courseRepo, string id) =>
{
    var existing = await courseRepo.GetByIDAsync(id);

    if (existing == null)
    {
        return Results.NotFound();
    }

    await courseRepo.DeleteAsync(id);

    return Results.NoContent();
});

await app.RunAsync();


