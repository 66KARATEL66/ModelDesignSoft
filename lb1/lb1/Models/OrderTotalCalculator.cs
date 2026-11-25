namespace lb1.Models;

public static class OrderTotalCalculator
{
    public static decimal Calculate(IEnumerable<OrderItem> orderItems)
    {
        ArgumentNullException.ThrowIfNull(orderItems);
        return orderItems.Sum(item => item.UnitPrice * item.Quantity);
    }
}
