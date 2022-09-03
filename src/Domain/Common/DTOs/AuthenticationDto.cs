using System.ComponentModel.DataAnnotations;

namespace Domain.Common.DTOs;

public class AuthenticationDto
{
    [Required]
    [EmailAddress]
    public string Username { get; set; }
    [Required]
    public string Password { get; set; }
}
