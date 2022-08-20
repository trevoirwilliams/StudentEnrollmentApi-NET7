using StudentEnrollment.Api.DTOs.Course;

namespace StudentEnrollment.Api.DTOs.Student
{
    public class StudentDetailsDto : CreateStudentDto
    {
        public List<CourseDto> Courses { get; set; } = new List<CourseDto>();
    }
}
