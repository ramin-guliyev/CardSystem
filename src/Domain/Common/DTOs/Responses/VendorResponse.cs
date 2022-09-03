namespace Domain.Common.DTOs.Responses;

public class VendorResponse
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public ICollection<AddressResponse> Addresses { get; set; }
}
