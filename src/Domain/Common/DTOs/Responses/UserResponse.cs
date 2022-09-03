using Domain.Entities;

namespace Domain.Common.DTOs.Responses;

public class UserResponse
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string UserName { get; set; }
    public ICollection<CardResponse> Cards { get; set; }
}
