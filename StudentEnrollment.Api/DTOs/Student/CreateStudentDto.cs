using FluentValidation;
using Microsoft.AspNetCore.Hosting;
using StudentEnrollment.Api.DTOs.Course;

namespace StudentEnrollment.Api.DTOs.Student
{
    public class CreateStudentDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? DateofBirth { get; set; }
        public string IdNumber { get; set; }
        public byte[] ProfilePicture { get; set; }
        public string OriginalFileName { get; set; }
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
                .LessThan(DateTime.Now)
                .NotEmpty();
            RuleFor(x => x.IdNumber)
                .NotEmpty();

            RuleFor(x => x.OriginalFileName)
                .NotNull()
                .When(x => x.ProfilePicture != null);
        }
    }
}
