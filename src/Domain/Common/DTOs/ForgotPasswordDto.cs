using System.ComponentModel.DataAnnotations;

namespace Domain.Common.DTOs;

public class ForgotPasswordDto
{
    [Required]
    [EmailAddress]
    public string Username { get; set; }
}
