namespace Domain.Entities;

public class Vendor : BaseEntity
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public ICollection<Address> Addresses { get; set; }
}
