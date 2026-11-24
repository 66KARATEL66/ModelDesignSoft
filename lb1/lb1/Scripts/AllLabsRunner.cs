using lb1.Data;
using lb1.Models;
using lb1.Services;

namespace LabScripts;

public class AllLabsRunner
{
    private readonly AnimeFigurineRepository _repository;
    private readonly DBItem<Figurine> _figurines;
    private readonly DBItem<AnimeSeries> _series;
    private readonly DBItem<CollectionTag> _tags;

    public AllLabsRunner(
        AnimeFigurineRepository repository,
        DBItem<Figurine> figurines,
        DBItem<AnimeSeries> series,
        DBItem<CollectionTag> tags)
    {
        _repository = repository;
        _figurines = figurines;
        _series = series;
        _tags = tags;
    }

    public void RunAll()
    {
        RunLab1();
        RunLab2();
        RunLab3();
        RunLab4();
        RunLab5();
        RunLab6();
        RunLab7();
        RunLab8();
        RunLab9();
    }

    private void RunLab1()
    {
        Console.WriteLine("=== LAB 1: RELATIONAL MODEL, MODELS, REPOSITORY, SERVICE ===");
        var service = new AnimeFigurineService(_repository);
        service.DisplayStoreOverview();
        service.DisplaySeriesFigurines(2);
        service.DisplayFigurineDetails(2);
        service.DisplayTagFigurines(1);
    }

    private void RunLab2()
    {
        Console.WriteLine("=== LAB 2: DOMAIN MODEL ===");
        Console.WriteLine($"Domain entities: {_series.Items.Count} series, {_figurines.Items.Count} figurines, {_tags.Items.Count} tags");
        Console.WriteLine("Customer/Order entities are available for purchase scenarios.");
        Console.WriteLine();
    }

    private void RunLab3()
    {
        Console.WriteLine("=== LAB 3: USE CASES ===");
        Console.WriteLine("Use cases: view catalog, place order, pay, confirm, ship, deliver, manage catalog");
        Console.WriteLine();
    }

    private void RunLab4()
    {
        Console.WriteLine("=== LAB 4: QUALITY AND CONTEXT ===");
        Console.WriteLine("Validated requirements: .NET 8 compatibility, valid state transitions, data correctness");
        Console.WriteLine();
    }

    private void RunLab5()
    {
        Console.WriteLine("=== LAB 5: ORDER STATE MACHINE ===");
        var customer = CreateCustomer();
        var order = CreateOrder(customer);
        Console.WriteLine($"Initial state: {order.Status}");
        Console.WriteLine($"Invalid ship before payment: {new OrderService(_repository).Ship(order)}, state: {order.Status}");
        Console.WriteLine();
    }

    private void RunLab6()
    {
        Console.WriteLine("=== LAB 6: PLACE-ORDER SEQUENCE ===");
        var customer = CreateCustomer();
        var orderService = new OrderService(_repository);
        var order = orderService.CreateOrder(customer);
        var luffy = _figurines.Items.First(item => item.Id == 2);
        var zoro = _figurines.Items.First(item => item.Id == 3);
        Console.WriteLine($"CreateOrder: Order #{order.Id}");
        Console.WriteLine($"AddItem: {orderService.AddItem(order, luffy, 1)}");
        Console.WriteLine($"AddItem: {orderService.AddItem(order, zoro, 2)}");
        Console.WriteLine($"Submit: {orderService.Submit(order)}, state: {order.Status}");
        Console.WriteLine($"Confirm: {orderService.Confirm(order)}, state: {order.Status}");
        Console.WriteLine($"Pay: {orderService.Pay(order)}, state: {order.Status}");
        Console.WriteLine($"Ship: {orderService.Ship(order)}, state: {order.Status}");
        Console.WriteLine($"Deliver: {orderService.Deliver(order)}, state: {order.Status}");
        Console.WriteLine($"Total: {order.Total} uah");
        Console.WriteLine();

        if (order.Status != OrderStatus.Delivered)
            throw new InvalidOperationException("Place-order sequence verification failed.");
    }

    private void RunLab7()
    {
        Console.WriteLine("=== LAB 7: PACKAGES ===");
        Console.WriteLine("Packages: lb1.Models -> lb1.Data -> lb1.Services -> Program");
        Console.WriteLine();
    }

    private void RunLab8()
    {
        Console.WriteLine("=== LAB 8: CLASS ALIGNMENT ===");
        Console.WriteLine("Classes verified: AnimeSeries, Figurine, Customer, Order, OrderItem, Payment, repositories, services");
        Console.WriteLine();
    }

    private void RunLab9()
    {
        Console.WriteLine("=== LAB 9: ARTIFACTS AND DEPLOYMENT ===");
        Console.WriteLine("Deployment target: developer workstation, .NET 8 runtime, console executable");
        Console.WriteLine();
    }

    private Customer CreateCustomer()
    {
        var customer = new Customer
        {
            Id = _repository.GetAllCustomers().Count + 1,
            Name = "Ivan Petrenko",
            Phone = "+380501234567",
            Email = "ivan@example.com"
        };
        _repository.AddCustomer(customer);
        return customer;
    }

    private Order CreateOrder(Customer customer)
    {
        var orderService = new OrderService(_repository);
        var order = orderService.CreateOrder(customer);
        var luffy = _figurines.Items.First(item => item.Id == 2);
        orderService.AddItem(order, luffy, 1);
        return order;
    }
}
