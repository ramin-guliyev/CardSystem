namespace Domain.Common.DTOs;

public class TransactionParams
{
    public DateTime Date { get; set; }
    public string CardNumber { get; set; }
    public double MinAmount { get; set; }
    public double MaxAmount { get; set; }
}
