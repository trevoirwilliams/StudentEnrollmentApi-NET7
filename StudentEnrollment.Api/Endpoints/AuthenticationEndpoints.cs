using AutoMapper;
using StudentEnrollment.Api.DTOs.Course;
using StudentEnrollment.Data.Contracts;
using StudentEnrollment.Data;
using Microsoft.AspNetCore.Identity;
using StudentEnrollment.Api.DTOs.Authentication;
using StudentEnrollment.Api.Services;
using StudentEnrollment.Api.DTOs;
using Microsoft.AspNetCore.Authorization;
using FluentValidation;
using Microsoft.AspNetCore.Http.HttpResults;

namespace StudentEnrollment.Api.Endpoints
{
    public static class AuthenticationEndpoints
    {
        public static void MapAuthenticationEndpoints(this IEndpointRouteBuilder routes)
        {
            routes.MapPost("/api/login/", async (LoginDto loginDto, IAuthManager authManager, IValidator<LoginDto> validator) =>
            {
                var validationResult = await validator.ValidateAsync(loginDto);

                if (!validationResult.IsValid)
                {
                    return Results.BadRequest(validationResult.ToDictionary());
                }

                var response = await authManager.Login(loginDto);

                if(response is null)
                {
                    return Results.Unauthorized();
                }

                return Results.Ok(response);

            })
            .AllowAnonymous()
            .WithTags("Authentication")
            .WithName("Login")
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status401Unauthorized);

            routes.MapPost("/api/register/", async (RegisterDto registerDto, IAuthManager authManager, IValidator<RegisterDto> validator) =>
            {

                var validationResult = await validator.ValidateAsync(registerDto);

                if (!validationResult.IsValid)
                {
                    return Results.BadRequest(validationResult.ToDictionary());
                }

                var response = await authManager.Register(registerDto);

                if (!response.Any())
                {
                    return Results.Ok();
                }

                var errors = new List<ErrorResponseDto>();
                foreach (var error in response)
                {
                    errors.Add(new ErrorResponseDto
                    {
                        Code = error.Code,
                        Description = error.Description
                    });
                }

                return Results.BadRequest(errors);

            })
            .AllowAnonymous()
            .WithTags("Authentication")
            .WithName("Register")
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest);
        }
    }
}
