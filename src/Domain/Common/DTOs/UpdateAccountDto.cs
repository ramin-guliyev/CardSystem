using Domain.Enums;

namespace Domain.Common.DTOs;

public class UpdateAccountDto
{
    public double Balance { get; set; }
    public AccountType Type { get; set; }
}