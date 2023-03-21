using Educative.Core.Entity;
using Educative.Infrastructure.Interface;
using Educative.Minimal.API.Dto;
using AutoMapper;
using Educative.Infrastructure.Repository;
using Educative.Minimal.API.Config;
using Educative.Minimal.API.Extension;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAutoMapper(typeof(MappingProfiles));
builder.Services.AddScoped<ICourseRepository, CourseRepository>();
builder.Services.AddScoped<IAddressRepository, AddressRepository>();
builder.Services.AddScoped<IStudentRepository, StudentRepository>();
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

app.MapGet("/addresses", async (IAddressRepository addressRepo, IMapper mapper) =>
{
    var addresses = await addressRepo.GetAllAsync();
    var result = mapper.Map<IEnumerable<AddressDto>>(addresses);
    return Results.Ok(result);

}).Produces<IEnumerable<AddressDto>>(StatusCodes.Status200OK)
.Produces<IEnumerable<AddressDto>>(StatusCodes.Status400BadRequest)
.Produces<IEnumerable<AddressDto>>(StatusCodes.Status500InternalServerError)
.WithName("GetAllAddress")
.WithTags("GetAPICalls");

app.MapGet("/students", async (IStudentRepository studentRepo, IMapper mapper) =>
{
    var students = await studentRepo.GetAllAsync();
    var result = mapper.Map<IEnumerable<StudentDto>>(students);

    return Results.Ok(result);

}).Produces<IEnumerable<StudentDto>>(StatusCodes.Status200OK)
.Produces<IEnumerable<StudentDto>>(StatusCodes.Status400BadRequest)
.Produces<IEnumerable<StudentDto>>(StatusCodes.Status500InternalServerError)
.WithName("GetAllStudent")
.WithTags("GetAPICalls");

app.MapGet("/courses", async (ICourseRepository courseRepo, IMapper mapper) =>
{
    var courses = await courseRepo.GetAllAsync();
    var result = mapper.Map<IEnumerable<CourseDto>>(courses);

    return Results.Ok(courses);

}).Produces<IEnumerable<CourseDto>>(StatusCodes.Status200OK)
.Produces<IEnumerable<CourseDto>>(StatusCodes.Status400BadRequest)
.Produces<IEnumerable<CourseDto>>(StatusCodes.Status500InternalServerError)
.WithName("GetAllCourses")
.WithTags("GetAPICalls");

app.MapGet("/courses/{id}", async (ICourseRepository courseRepo, IMapper mapper, string id) =>
{
    var course = await courseRepo.GetByIDAsync(id);
    var result = mapper.Map<CourseDto>(course);

    return Results.Ok(result);
}).Produces<CourseDto>(StatusCodes.Status200OK)
.Produces<CourseDto>(StatusCodes.Status404NotFound)
.Produces<CourseDto>(StatusCodes.Status500InternalServerError)
.WithName("GetCourseByID")
.WithTags("GetAPICalls");

app.MapPost("/student", async (IStudentRepository studentRepo, IMapper mapper, StudentDto studentDto) =>
{
    if (studentDto == null)
    {
        return Results.BadRequest();
    }

    var student = mapper.Map<Student>(studentDto);
    await studentRepo.AddAsync(student);

    var createdStudent = mapper.Map<StudentDto>(student);

    return Results.CreatedAtRoute("GetStudentByID", new { id = createdStudent.StudentID.ToLower() }, createdStudent);
}).Produces<StudentDto>(StatusCodes.Status201Created)
.Produces<StudentDto>(StatusCodes.Status400BadRequest)
.Produces<StudentDto>(StatusCodes.Status500InternalServerError)
.WithName("AddStudent")
.WithTags("SetAPICalls");

app.MapPost("/course", async (ICourseRepository courseRepo, IMapper mapper, CourseDto courseDto) =>
{
    if (courseDto == null)
    {
        return Results.BadRequest();
    }

    var course = mapper.Map<Course>(courseDto);
    await courseRepo.AddAsync(course);

    var createdCourse = mapper.Map<CourseDto>(course);

    return Results.CreatedAtRoute("GetCourseByID", new { id = createdCourse.CourseID.ToLower() }, createdCourse);
}).Produces<CourseDto>(StatusCodes.Status201Created)
.Produces<CourseDto>(StatusCodes.Status400BadRequest)
.Produces<CourseDto>(StatusCodes.Status500InternalServerError)
    .Produces<CourseDto>()
    .WithName("AddCourse")
    .WithTags("SetAPICalls");

app.MapPut("/course/{id}", async (ICourseRepository courseRepo, IMapper mapper, string id, CourseDto courseDto) =>
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
}).Produces<CourseDto>(StatusCodes.Status204NoContent)
.Produces<CourseDto>(StatusCodes.Status404NotFound)
.Produces<CourseDto>(StatusCodes.Status400BadRequest)
.Produces<CourseDto>(StatusCodes.Status500InternalServerError)
.WithName("UpdateCourse")
.WithTags("SetAPICalls");

app.MapDelete("/course/{id}", async (ICourseRepository courseRepo, string id) =>
{
    var existing = await courseRepo.GetByIDAsync(id);

    if (existing == null)
    {
        return Results.NotFound();
    }

    await courseRepo.DeleteAsync(id);

    return Results.NoContent();
}).Produces<CourseDto>(StatusCodes.Status204NoContent)
.Produces<CourseDto>(StatusCodes.Status404NotFound)
.Produces<CourseDto>(StatusCodes.Status500InternalServerError)
.WithName("DeleteCourse")
.WithTags("Remove");

await app.RunAsync();


