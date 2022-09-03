using Domain.Enums;

namespace Domain.Common.DTOs;

public class CreateAccountDto
{
    public double Balance { get; set; }
    public AccountType Type { get; set; }
}
