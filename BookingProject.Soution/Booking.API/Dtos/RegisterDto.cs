using System.ComponentModel.DataAnnotations;

namespace NotesProjects.Dtos
{
    public class RegisterDto
    {
        [Required]
        public string DisplayName { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        public string Age { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
