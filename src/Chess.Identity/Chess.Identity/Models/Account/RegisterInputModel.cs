using System.ComponentModel.DataAnnotations;

namespace Chess.Identity.Models.Account
{
    public class RegisterInputModel
    {
        [Required]
        public string Username { get; set; }

        [EmailAddress]
        public string Email { get; set; }
        
        [Required]
        public string Password { get; set; }

        [Required]
        [Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }
        
        public string ReturnUrl { get; set; }
    }
}