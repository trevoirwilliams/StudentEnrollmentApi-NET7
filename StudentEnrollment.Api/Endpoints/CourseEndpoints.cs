using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StudentEnrollment.Api.DTOs.Course;
using StudentEnrollment.Data;
using StudentEnrollment.Data.Contracts;

namespace StudentEnrollment.Api.Endpoints;

public static class CourseEndpoints
{
    public static void MapCourseEndpoints (this IEndpointRouteBuilder routes)
    {
        routes.MapGet("/api/Course", async (ICourseRepository repo, IMapper mapper) =>
        {
            var courses = await repo.GetAllAsync();
            var data = mapper.Map<List<CourseDto>>(courses);
            return data;
        })
        .WithTags(nameof(Course))
        .WithName("GetAllCourses")
        .Produces<List<CourseDto>>(StatusCodes.Status200OK);

        routes.MapGet("/api/Course/{id}", async (int Id, ICourseRepository repo, IMapper mapper) =>
        {
            return await repo.GetAsync(Id)
                is Course model
                    ? Results.Ok(mapper.Map<CourseDto>(model))
                    : Results.NotFound();
        })
        .WithTags(nameof(Course))
        .WithName("GetCourseById")
        .Produces<CourseDto>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);

        routes.MapGet("/api/Course/GetStudents/{id}", async (int Id, ICourseRepository repo, IMapper mapper) =>
        {
            return await repo.GetStudentList(Id)
                is Course model
                    ? Results.Ok(mapper.Map<CourseDetailsDto>(model))
                    : Results.NotFound();
        })
        .WithTags(nameof(Course))
        .WithName("GetCourseDetailsById")
        .Produces<CourseDetailsDto>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);

        routes.MapPut("/api/Course/{id}", async (int Id, CourseDto courseDto, ICourseRepository repo, IMapper mapper) =>
        {
            var foundModel = await repo.GetAsync(Id);

            if (foundModel is null)
            {
                return Results.NotFound();
            }
            //update model properties here
            mapper.Map(courseDto, foundModel);
            await repo.UpdateAsync(foundModel);

            return Results.NoContent();
        })
        .WithTags(nameof(Course))
        .WithName("UpdateCourse")
        .Produces(StatusCodes.Status404NotFound)
        .Produces(StatusCodes.Status204NoContent);

        routes.MapPost("/api/Course/", async (CreateCourseDto courseDto, ICourseRepository repo, IMapper mapper) =>
        {
            var course = mapper.Map<Course>(courseDto);
            await repo.AddAsync(course);
            return Results.Created($"/Courses/{course.Id}", course);
        })
        .WithTags(nameof(Course))
        .WithName("CreateCourse")
        .Produces<Course>(StatusCodes.Status201Created);

        routes.MapDelete("/api/Course/{id}", async (int Id, ICourseRepository repo) =>
        {
            return await repo.DeleteAsync(Id) ? Results.NoContent() : Results.NotFound();
        })
        .WithTags(nameof(Course))
        .WithName("DeleteCourse")
        .Produces<Course>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);
    }
}
