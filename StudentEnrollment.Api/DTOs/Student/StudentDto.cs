using FluentValidation;

namespace StudentEnrollment.Api.DTOs.Student
{
    public class StudentDto : CreateStudentDto
    {
        public int Id { get; set; }
    }

    public class StudentDtoValidator : AbstractValidator<StudentDto>
    {
        public StudentDtoValidator()
        {
            Include(new CreateStudentDtoValidator());
        }
    }
}
