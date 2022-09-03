using System.ComponentModel.DataAnnotations;

namespace Domain.Common.DTOs;

public class ResetPasswordDto
{
    [Required]
    [EmailAddress]
    public string UserName { get; set; }
    [Required]
    public string Token { get; set; }
    [Required]
    public string Password { get; set; }
    [Required]
    [Compare("Password")]
    public string ConfirmPassword { get; set; }
}