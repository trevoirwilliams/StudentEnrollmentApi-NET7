using FluentValidation;
using StudentEnrollment.Api.DTOs.Course;
using StudentEnrollment.Api.DTOs.Student;
using StudentEnrollment.Data.Contracts;

namespace StudentEnrollment.Api.DTOs.Enrollment
{
    public class EnrollmentDto : CreateEnrollmentDto
    {
        public virtual CourseDto Course { get; set; }
        public virtual StudentDto Student { get; set; }
    }

    public class EnrollmentDtoValidator : AbstractValidator<EnrollmentDto>
    {
        public EnrollmentDtoValidator(ICourseRepository courseRepository, IStudentRepository studentRepository)
        {
            Include(new CreateEnrollmentDtoValidator(courseRepository, studentRepository));
        }
    }
}
