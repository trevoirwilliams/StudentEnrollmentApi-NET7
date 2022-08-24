using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using NuGet.Configuration;
using StudentEnrollment.Api.DTOs.Enrollment;
using StudentEnrollment.Data;
using StudentEnrollment.Data.Contracts;
using System.Data;

namespace StudentEnrollment.Api.Endpoints;

public static class EnrollmentEndpoints
{
    public static void MapEnrollmentEndpoints(this IEndpointRouteBuilder routes)
    {
        routes.MapGet("/api/Enrollment", async (IEnrollmentRepository repo, IMapper mapper) =>
        {
            var enrollments = await repo.GetAllAsync();
            var data = mapper.Map<List<EnrollmentDto>>(enrollments);
            return data;
        })
        .WithTags(nameof(Enrollment))
        .WithName("GetAllEnrollments")
        .Produces<List<EnrollmentDto>>(StatusCodes.Status200OK);

        routes.MapGet("/api/Enrollment/{id}", async (int Id, IEnrollmentRepository repo, IMapper mapper) =>
        {
            return await repo.GetAsync(Id)
                is Enrollment model
                    ? Results.Ok(mapper.Map<EnrollmentDto>(model))
                    : Results.NotFound();
        })
        .WithTags(nameof(Enrollment))
        .WithName("GetEnrollmentById")
        .Produces<EnrollmentDto>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);

        routes.MapPut("/api/Enrollment/{id}", [Authorize(Roles = "Administrator")] async (int Id, EnrollmentDto enrollmentDto, IEnrollmentRepository repo, IMapper mapper) =>
        {
            var foundModel = await repo.GetAsync(Id);

            if (foundModel is null)
            {
                return Results.NotFound();
            }
            //update model properties here
            mapper.Map(enrollmentDto, foundModel);
            await repo.UpdateAsync(foundModel);

            return Results.NoContent();
        })
        .WithTags(nameof(Enrollment))
        .WithName("UpdateEnrollment")
        .Produces(StatusCodes.Status404NotFound)
        .Produces(StatusCodes.Status204NoContent);

        routes.MapPost("/api/Enrollment/", async (CreateEnrollmentDto enrollmentDto, IEnrollmentRepository repo, IMapper mapper) =>
        {
            var enrollment = mapper.Map<Enrollment>(enrollmentDto);
            await repo.AddAsync(enrollment);
            return Results.Created($"/Enrollments/{enrollment.Id}", enrollment);
        })
        .WithTags(nameof(Enrollment))
        .WithName("CreateEnrollment")
        .Produces<Enrollment>(StatusCodes.Status201Created);

        routes.MapDelete("/api/Enrollment/{id}", [Authorize(Roles = "Administrator")] async (int Id, IEnrollmentRepository repo, IMapper mapper) =>
        {
            return await repo.DeleteAsync(Id) ? Results.NoContent() : Results.NotFound();
        })
        .WithTags(nameof(Enrollment))
        .WithName("DeleteEnrollment")
        .Produces<Enrollment>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);
    }
}
