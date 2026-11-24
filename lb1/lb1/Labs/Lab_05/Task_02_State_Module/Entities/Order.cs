using lb1.Data;

namespace lb1.Models;

public enum OrderStatus
{
    Draft,
    Submitted,
    Confirmed,
    Paid,
    Shipped,
    Delivered,
    Cancelled
}

public class Order : IEntity
{
    private readonly HashSet<OrderStatus> _terminalStatuses = new()
    {
        OrderStatus.Delivered,
        OrderStatus.Cancelled
    };

    public int Id { get; set; }
    public int CustomerId { get; set; }
    public Customer? Customer { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public OrderStatus Status { get; private set; } = OrderStatus.Draft;
    public List<OrderItem> Items { get; } = new();
    public Payment? Payment { get; set; }

    public decimal Total => Items.Sum(item => item.UnitPrice * item.Quantity);

    public bool Submit()
    {
        if (Status != OrderStatus.Draft || Items.Count == 0)
            return false;

        Status = OrderStatus.Submitted;
        return true;
    }

    public bool Confirm()
    {
        if (Status != OrderStatus.Submitted)
            return false;

        Status = OrderStatus.Confirmed;
        return true;
    }

    public bool Pay()
    {
        if (Status != OrderStatus.Confirmed)
            return false;

        Status = OrderStatus.Paid;
        return true;
    }

    public bool Ship()
    {
        if (Status != OrderStatus.Paid)
            return false;

        Status = OrderStatus.Shipped;
        return true;
    }

    public bool Deliver()
    {
        if (Status != OrderStatus.Shipped)
            return false;

        Status = OrderStatus.Delivered;
        return true;
    }

    public bool Cancel()
    {
        if (_terminalStatuses.Contains(Status) || Status == OrderStatus.Shipped)
            return false;

        Status = OrderStatus.Cancelled;
        return true;
    }
}
