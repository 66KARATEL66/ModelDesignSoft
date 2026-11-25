using lb1.Data;
using lb1.Models;

namespace lb1.Services;

public sealed class OrderService
{
    private readonly AnimeFigurineRepository _repository;

    public OrderService(AnimeFigurineRepository repository)
    {
        _repository = repository;
    }

    public Order CreateOrder(Customer customer) => _repository.CreateOrder(customer);

    public bool AddItem(Order order, Figurine figurine, int quantity) =>
        _repository.AddOrderItem(order, figurine, quantity) is not null;

    public bool Submit(Order order) => order.Submit();
    public bool Confirm(Order order) => order.Confirm();

    public bool Pay(Order order)
    {
        if (order.Payment is not null || !order.Pay())
            return false;

        _repository.AddCompletedPayment(order);
        return true;
    }

    public bool Ship(Order order) => order.Ship();
    public bool Deliver(Order order) => order.Deliver();
    public bool Cancel(Order order) => order.Cancel();
}
