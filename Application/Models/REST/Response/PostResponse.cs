namespace Application.Models.REST.Response;

public class PostResponse
{
    public decimal Price { get; set; }
    public string Description { get; set; } = String.Empty;
    public Guid PostOwner { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}