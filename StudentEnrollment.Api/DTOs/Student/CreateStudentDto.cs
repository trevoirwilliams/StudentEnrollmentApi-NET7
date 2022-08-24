using FluentValidation;
using StudentEnrollment.Api.DTOs.Course;

namespace StudentEnrollment.Api.DTOs.Student
{
    public class CreateStudentDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateofBirth { get; set; }
        public string IdNumber { get; set; }
        public string Picture { get; set; }
    }

    public class CreateStudentDtoValidator : AbstractValidator<CreateStudentDto>
    {
        public CreateStudentDtoValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty();
            RuleFor(x => x.LastName)
                .NotEmpty();
            RuleFor(x => x.DateofBirth)
                .NotEmpty();
            RuleFor(x => x.IdNumber)
                .NotEmpty();
        }
    }
}
