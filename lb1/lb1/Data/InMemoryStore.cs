using lb1.Models;

namespace lb1.Data;

public sealed class InMemoryStore
{
    public List<AnimeSeries> Series { get; } = new();
    public List<Figurine> Figurines { get; } = new();
    public List<FigurineSpecification> Specifications { get; } = new();
    public List<CollectionTag> Tags { get; } = new();
    public List<FigurineTag> FigurineTags { get; } = new();
    public List<Customer> Customers { get; } = new();
    public List<Order> Orders { get; } = new();
    public List<OrderItem> OrderItems { get; } = new();
    public List<Payment> Payments { get; } = new();
}
