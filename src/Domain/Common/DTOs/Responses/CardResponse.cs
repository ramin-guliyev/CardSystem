using Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Domain.Common.DTOs.Responses;

public class CardResponse
{
    public int Id { get; set; }
    public string Number { get; set; }
    public bool Valid { get; set; }
    public string State { get; set; }
    public string Type { get; set; }
    public string Currency { get; set; }
    public string User { get; set; }
    public int UserId { get; set; }
}
