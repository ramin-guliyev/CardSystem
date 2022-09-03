using Domain.Enums;

namespace Domain.Common.DTOs;

public class CreateTransactionDto
{
    public decimal Amount { get; set; }
    public string CardNumber { get; set; }
    public CreateVendorDto Vendor { get; set; }
}
public class CreateVendorDto
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public List<CreateAddressDto> Addresses { get; set; }
}
public class CreateAddressDto
{
    public string City { get; set; }
    public string Street { get; set; }
    public string ZipCode { get; set; }
}