using System.ComponentModel.DataAnnotations;

namespace DatingApp.api.Dtos
{
    public class UserForRegisterDto
    {
        [Required]
        public string Username { get; set; }

        [Required]
        [StringLength(9, MinimumLength = 4, ErrorMessage = "Specify pwd between 4-9 characters")]
        public string Password { get; set; }
    }
}