using System.ComponentModel.DataAnnotations;

namespace StudentEnrollment.Api.DTOs.Authentication
{
    public class LoginDto
    {
        public string EmailAddress { get; set; }
        public string Password { get; set; }
    }
}
