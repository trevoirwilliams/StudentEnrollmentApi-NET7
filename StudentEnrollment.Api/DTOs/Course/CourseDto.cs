using FluentValidation;
using System.ComponentModel.DataAnnotations;

namespace StudentEnrollment.Api.DTOs.Course
{
    public class CourseDto : CreateCourseDto
    {
        public int Id { get; set; }
    }

    public class CourseDtoValidator : AbstractValidator<CourseDto>
    {
        public CourseDtoValidator()
        {
            Include(new CreateCourseDtoValidator());
        }
    }
}
