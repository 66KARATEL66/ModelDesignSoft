using lb1.Data;
using lb1.Models;

namespace lb1.Services;

public class OrderService
{
    private readonly AnimeFigurineRepository _repository;

    public OrderService(AnimeFigurineRepository repository)
    {
        _repository = repository;
    }

    public Order CreateOrder(Customer customer)
    {
        var order = new Order
        {
            Id = _repository.GetAllOrders().Count + 1,
            CustomerId = customer.Id,
            Customer = customer
        };
        customer.Orders.Add(order);
        _repository.AddOrder(order);
        return order;
    }

    public bool AddItem(Order order, Figurine figurine, int quantity)
    {
        if (order.Status != OrderStatus.Draft || quantity <= 0)
            return false;

        order.Items.Add(new OrderItem
        {
            Id = order.Items.Count + 1,
            OrderId = order.Id,
            FigurineId = figurine.Id,
            Figurine = figurine,
            Quantity = quantity,
            UnitPrice = figurine.Price
        });
        return true;
    }

    public bool Submit(Order order) => order.Submit();
    public bool Confirm(Order order) => order.Confirm();
    public bool Pay(Order order)
    {
        if (!order.Pay())
            return false;

        order.Payment = new Payment
        {
            Id = _repository.GetAllPayments().Count + 1,
            OrderId = order.Id,
            Amount = order.Total,
            Status = PaymentStatus.Completed,
            PaidAt = DateTime.UtcNow
        };
        _repository.AddPayment(order.Payment);
        return true;
    }

    public bool Ship(Order order) => order.Ship();
    public bool Deliver(Order order) => order.Deliver();
    public bool Cancel(Order order) => order.Cancel();
}
