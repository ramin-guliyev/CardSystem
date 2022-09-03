using Domain.Enums;

namespace Domain.Entities;

public class Account : BaseEntity
{
    public double Balance { get; set; }
    public AccountType Type { get; set; }
    public User User { get; set; }
}
