using StudentEnrollment.Api.DTOs.Course;
using StudentEnrollment.Api.DTOs.Student;

namespace StudentEnrollment.Api.DTOs.Enrollment
{
    public class EnrollmentDto
    {
        public int CourseId { get; set; }
        public int StudentId { get; set; }

        public virtual CourseDto Course { get; set; }
        public virtual StudentDto Student { get; set; }
    }
}
