using StudentEnrollment.Data.Contracts;

namespace StudentEnrollment.Data.Repositories
{
    public class EnrollmentRepository : GenericRepository<Enrollment>, IEnrollmentRepository
    {
        public EnrollmentRepository(StudentEnrollmentDbContext db) : base(db)
        {
        }
    }
}
