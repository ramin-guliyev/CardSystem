using Microsoft.AspNetCore.Identity;

namespace Domain.Entities;
public class User : IdentityUser<int>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime LastLogin { get; set; }
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    public DateTime LastPasswordChange { get; set; }
    public ICollection<Account> Accounts { get; set; }
    public ICollection<Transaction> Transactions { get; set; }
    public ICollection<Card> Cards { get; set; }
}
