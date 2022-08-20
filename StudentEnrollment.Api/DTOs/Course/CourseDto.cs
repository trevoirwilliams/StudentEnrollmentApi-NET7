using System.ComponentModel.DataAnnotations;

namespace StudentEnrollment.Api.DTOs.Course
{
    public class CourseDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Credits { get; set; }
    }
}
