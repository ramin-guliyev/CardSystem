using System.ComponentModel.DataAnnotations;

namespace Domain.Common.DTOs;

public class ChangeUserNameDto
{
    [Required]
    [EmailAddress]
    public string UserName { get; set; }
}
