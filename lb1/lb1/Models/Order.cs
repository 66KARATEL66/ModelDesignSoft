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

public sealed class Order
{
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public Customer? Customer { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public OrderStatus Status { get; private set; } = OrderStatus.Draft;
    public List<OrderItem> Items { get; } = new();
    public Payment? Payment { get; set; }
    public decimal Total => OrderTotalCalculator.Calculate(Items);

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
        if (Status is OrderStatus.Shipped or OrderStatus.Delivered or OrderStatus.Cancelled)
            return false;

        Status = OrderStatus.Cancelled;
        return true;
    }
}
