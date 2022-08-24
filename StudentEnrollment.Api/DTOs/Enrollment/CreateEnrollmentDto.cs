using FluentValidation;
using StudentEnrollment.Api.DTOs.Course;
using StudentEnrollment.Data.Contracts;

namespace StudentEnrollment.Api.DTOs.Enrollment
{
    public class CreateEnrollmentDto
    {
        public int CourseId { get; set; }
        public int StudentId { get; set; }
    }

    public class CreateEnrollmentDtoValidator : AbstractValidator<CreateEnrollmentDto>
    {
        private readonly ICourseRepository _courseRepository;
        private readonly IStudentRepository _studentRepository;

        public CreateEnrollmentDtoValidator(ICourseRepository courseRepository, IStudentRepository studentRepository)
        {
            this._courseRepository = courseRepository;
            this._studentRepository = studentRepository;

            RuleFor(x => x.CourseId)
                .MustAsync(async (id, token) =>
                {
                    var courseExists = await _courseRepository.Exists(id);
                    return courseExists;
                }).WithMessage("{PropertyName} does not exist");
            RuleFor(x => x.StudentId)
                .MustAsync(async (id, token) =>
                {
                    var studentExists = await _studentRepository.Exists(id);
                    return studentExists;
                }).WithMessage("{PropertyName} does not exist");
        }
    }
}
