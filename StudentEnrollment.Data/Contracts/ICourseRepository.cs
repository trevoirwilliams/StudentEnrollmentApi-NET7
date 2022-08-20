namespace StudentEnrollment.Data.Contracts
{
    public interface ICourseRepository : IGenericRepository<Course>
    {
        Task<Course> GetStudentList(int courseId);
    }
}
