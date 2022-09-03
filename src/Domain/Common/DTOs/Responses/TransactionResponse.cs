namespace Domain.Common.DTOs.Responses;

public class TransactionResponse
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public string Type { get; set; }
    public decimal Amount { get; set; }
    public string CardNumber { get; set; }
    public string User { get; set; }
    public int UserId { get; set; }
    public VendorResponse Vendor { get; set; }
}
