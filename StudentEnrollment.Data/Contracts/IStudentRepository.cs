using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentEnrollment.Data.Contracts
{
    public interface IStudentRepository : IGenericRepository<Student>
    {
        Task<Student> GetStudentDetails(int studentId);
    }
}
