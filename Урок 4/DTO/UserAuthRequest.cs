using System.ComponentModel.DataAnnotations;

namespace DTO
{
    public class UserAuthRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        public RoleType UserRole { get; set; } = RoleType.User;
    }
}
