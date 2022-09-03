namespace Domain.Entities;

public class Address : BaseEntity
{
    public string City { get; set; }
    public string Street { get; set; }
    public string ZipCode { get; set; }
    public Vendor Vendor { get; set; }
}
