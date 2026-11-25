using lb1.Models;

namespace lb1.Data;

public sealed class AnimeFigurineRepository
{
    private readonly InMemoryStore _store;

    public AnimeFigurineRepository(InMemoryStore? store = null)
    {
        _store = store ?? new InMemoryStore();
    }

    public IReadOnlyList<AnimeSeries> GetAllSeries() => _store.Series.AsReadOnly();
    public IReadOnlyList<Figurine> GetAllFigurines() => _store.Figurines.AsReadOnly();
    public IReadOnlyList<FigurineSpecification> GetAllSpecifications() => _store.Specifications.AsReadOnly();
    public IReadOnlyList<CollectionTag> GetAllTags() => _store.Tags.AsReadOnly();
    public IReadOnlyList<FigurineTag> GetAllFigurineTags() => _store.FigurineTags.AsReadOnly();
    public IReadOnlyList<Customer> GetAllCustomers() => _store.Customers.AsReadOnly();
    public IReadOnlyList<Order> GetAllOrders() => _store.Orders.AsReadOnly();
    public IReadOnlyList<OrderItem> GetAllOrderItems() => _store.OrderItems.AsReadOnly();
    public IReadOnlyList<Payment> GetAllPayments() => _store.Payments.AsReadOnly();

    public AnimeSeries? GetSeriesById(int id) => _store.Series.FirstOrDefault(series => series.Id == id);
    public Figurine? GetFigurineById(int id) => _store.Figurines.FirstOrDefault(figurine => figurine.Id == id);
    public CollectionTag? GetTagById(int id) => _store.Tags.FirstOrDefault(tag => tag.Id == id);
    public Customer? GetCustomerById(int id) => _store.Customers.FirstOrDefault(customer => customer.Id == id);
    public Order? GetOrderById(int id) => _store.Orders.FirstOrDefault(order => order.Id == id);

    public Customer? GetCustomerByEmail(string email) =>
        _store.Customers.FirstOrDefault(customer =>
            string.Equals(customer.Email, email, StringComparison.OrdinalIgnoreCase));

    public IReadOnlyList<Order> GetOrdersByCustomerId(int customerId) =>
        _store.Orders.Where(order => order.CustomerId == customerId).ToArray();

    public IReadOnlyList<Figurine> GetFigurinesBySeriesId(int seriesId) =>
        _store.Figurines.Where(figurine => figurine.AnimeSeriesId == seriesId).ToArray();

    public IReadOnlyList<Figurine> GetFigurinesByTagId(int tagId) =>
        _store.FigurineTags
            .Where(link => link.TagId == tagId)
            .Select(link => GetFigurineById(link.FigurineId))
            .OfType<Figurine>()
            .ToArray();

    public IReadOnlyList<CollectionTag> GetTagsByFigurineId(int figurineId) =>
        _store.FigurineTags
            .Where(link => link.FigurineId == figurineId)
            .Select(link => GetTagById(link.TagId))
            .OfType<CollectionTag>()
            .ToArray();

    public AnimeSeries AddSeries(string title, string studio)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Назва серії обов'язкова.", nameof(title));

        var series = new AnimeSeries
        {
            Id = NextId(_store.Series, item => item.Id),
            Title = title.Trim(),
            Studio = studio.Trim()
        };
        _store.Series.Add(series);
        return series;
    }

    public Figurine AddFigurine(string name, int price, int seriesId)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Назва фігурки обов'язкова.", nameof(name));
        if (price <= 0)
            throw new ArgumentOutOfRangeException(nameof(price), "Ціна має бути більшою за нуль.");

        var series = GetSeriesById(seriesId)
            ?? throw new ArgumentException("Серію не знайдено.", nameof(seriesId));
        var figurine = new Figurine
        {
            Id = NextId(_store.Figurines, item => item.Id),
            Name = name.Trim(),
            Price = price,
            AnimeSeriesId = series.Id,
            AnimeSeries = series
        };
        _store.Figurines.Add(figurine);
        series.Figurines.Add(figurine);
        return figurine;
    }

    public FigurineSpecification AddSpecification(
        int figurineId,
        decimal heightCm,
        string material,
        bool limitedEdition)
    {
        var figurine = GetFigurineById(figurineId)
            ?? throw new ArgumentException("Фігурку не знайдено.", nameof(figurineId));
        if (figurine.Specification is not null)
            throw new InvalidOperationException("Для цієї фігурки специфікацію вже створено.");
        if (heightCm <= 0)
            throw new ArgumentOutOfRangeException(nameof(heightCm), "Висота має бути більшою за нуль.");
        if (string.IsNullOrWhiteSpace(material))
            throw new ArgumentException("Матеріал обов'язковий.", nameof(material));

        var specification = new FigurineSpecification
        {
            Id = NextId(_store.Specifications, item => item.Id),
            FigurineId = figurine.Id,
            Figurine = figurine,
            HeightCm = heightCm,
            Material = material.Trim(),
            LimitedEdition = limitedEdition
        };
        figurine.Specification = specification;
        _store.Specifications.Add(specification);
        return specification;
    }

    public CollectionTag AddTag(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Назва мітки обов'язкова.", nameof(name));
        if (_store.Tags.Any(tag => string.Equals(tag.Name, name.Trim(), StringComparison.OrdinalIgnoreCase)))
            throw new InvalidOperationException("Мітка з такою назвою вже існує.");

        var tag = new CollectionTag
        {
            Id = NextId(_store.Tags, item => item.Id),
            Name = name.Trim()
        };
        _store.Tags.Add(tag);
        return tag;
    }

    public bool AssignTag(int figurineId, int tagId)
    {
        var figurine = GetFigurineById(figurineId);
        var tag = GetTagById(tagId);
        if (figurine is null || tag is null ||
            _store.FigurineTags.Any(link => link.FigurineId == figurineId && link.TagId == tagId))
            return false;

        _store.FigurineTags.Add(new FigurineTag { FigurineId = figurineId, TagId = tagId });
        figurine.Tags.Add(tag);
        tag.Figurines.Add(figurine);
        return true;
    }

    public Customer AddCustomer(string name, string phone, string email)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Ім'я покупця обов'язкове.", nameof(name));
        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentException("Email покупця обов'язковий.", nameof(email));
        if (GetCustomerByEmail(email.Trim()) is not null)
            throw new InvalidOperationException("Покупець із таким email вже існує.");

        var customer = new Customer
        {
            Id = NextId(_store.Customers, item => item.Id),
            Name = name.Trim(),
            Phone = phone.Trim(),
            Email = email.Trim()
        };
        _store.Customers.Add(customer);
        return customer;
    }

    public Order CreateOrder(Customer customer)
    {
        if (!_store.Customers.Contains(customer))
            throw new ArgumentException("Покупця не знайдено в системі.", nameof(customer));

        var order = new Order
        {
            Id = NextId(_store.Orders, item => item.Id),
            CustomerId = customer.Id,
            Customer = customer
        };
        _store.Orders.Add(order);
        customer.Orders.Add(order);
        return order;
    }

    public OrderItem? AddOrderItem(Order order, Figurine figurine, int quantity)
    {
        if (!_store.Orders.Contains(order) || !_store.Figurines.Contains(figurine) ||
            order.Status != OrderStatus.Draft || quantity <= 0)
            return null;

        var item = new OrderItem
        {
            Id = NextId(_store.OrderItems, entry => entry.Id),
            OrderId = order.Id,
            FigurineId = figurine.Id,
            Figurine = figurine,
            Quantity = quantity,
            UnitPrice = figurine.Price
        };
        order.Items.Add(item);
        _store.OrderItems.Add(item);
        return item;
    }

    public Payment AddCompletedPayment(Order order)
    {
        if (!_store.Orders.Contains(order))
            throw new ArgumentException("Замовлення не знайдено.", nameof(order));
        if (order.Payment is not null)
            throw new InvalidOperationException("Для замовлення вже існує платіж.");

        var payment = new Payment
        {
            Id = NextId(_store.Payments, item => item.Id),
            OrderId = order.Id,
            Amount = order.Total,
            Status = PaymentStatus.Completed,
            PaidAt = DateTime.UtcNow
        };
        order.Payment = payment;
        _store.Payments.Add(payment);
        return payment;
    }

    private static int NextId<T>(IEnumerable<T> items, Func<T, int> idSelector) =>
        items.Select(idSelector).DefaultIfEmpty().Max() + 1;
}
