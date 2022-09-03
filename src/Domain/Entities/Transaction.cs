using Domain.Enums;

namespace Domain.Entities;

public class Transaction : BaseEntity
{
    public DateTime Date { get; set; }
    public TransactionType Type { get; set; }
    public decimal Amount { get; set; }
    public string CardNumber { get; set; }
    public User User { get; set; }
    public Vendor Vendor { get; set; }
}
