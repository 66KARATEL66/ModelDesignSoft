using lb1.Data;

namespace lb1.Models;

public class Customer : IEntity
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public List<Order> Orders { get; } = new();
}
