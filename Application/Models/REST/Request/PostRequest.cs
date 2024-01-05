namespace Application.Models.REST.Request;

public class PostRequest
{
    public string Description { get; set; } = String.Empty;
    public decimal Price { get; set; }
    
}