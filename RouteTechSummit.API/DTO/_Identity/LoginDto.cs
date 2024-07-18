using System.ComponentModel.DataAnnotations;

namespace RouteTechSummit.API.DTO._Identity
{
    public class LoginDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;

        [Required]
        public string Password { get; set; } = null!;
    }
}
