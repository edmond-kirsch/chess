using System.ComponentModel.DataAnnotations;

namespace Chess.API.Models;

public class RegisterModel
{
    [Required]
    public string Username { get; set; }
    [Required]
    public string Email { get; set; }
    [Required]
    public string Password { get; set; }
    [Required]
    [Compare(nameof(Password))]
    public string PasswordConfirmation { get; set; }
}