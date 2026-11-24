using lb1.Models;

namespace lb1.Data;

public class AnimeFigurineRepository
{
    private readonly DBItem<AnimeSeries> _series;
    private readonly DBItem<Figurine> _figurines;
    private readonly DBItem<CollectionTag> _tags;
    private readonly DBItem<FigurineTag> _figurineTags;
    private readonly DBItem<Customer> _customers = new();
    private readonly DBItem<Order> _orders = new();
    private readonly DBItem<Payment> _payments = new();

    public AnimeFigurineRepository(
        DBItem<AnimeSeries> series,
        DBItem<Figurine> figurines,
        DBItem<CollectionTag> tags,
        DBItem<FigurineTag> figurineTags)
    {
        _series = series;
        _figurines = figurines;
        _tags = tags;
        _figurineTags = figurineTags;
    }

    public IReadOnlyList<AnimeSeries> GetAllSeries() => _series.Items;
    public IReadOnlyList<Figurine> GetAllFigurines() => _figurines.Items;
    public IReadOnlyList<CollectionTag> GetAllTags() => _tags.Items;
    public IReadOnlyList<Customer> GetAllCustomers() => _customers.Items;
    public IReadOnlyList<Order> GetAllOrders() => _orders.Items;
    public IReadOnlyList<Payment> GetAllPayments() => _payments.Items;

    public void AddCustomer(Customer customer) => _customers.Add(customer);
    public void AddOrder(Order order) => _orders.Add(order);
    public void AddPayment(Payment payment) => _payments.Add(payment);

    public AnimeSeries? GetSeriesById(int id) => _series.Items.FirstOrDefault(item => item.Id == id);

    public Figurine? GetFigurineById(int id) => _figurines.Items.FirstOrDefault(item => item.Id == id);

    public CollectionTag? GetTagById(int id) => _tags.Items.FirstOrDefault(item => item.Id == id);

    public IReadOnlyList<Figurine> GetFigurinesBySeriesId(int seriesId) =>
        _figurines.Items.Where(figurine => figurine.AnimeSeriesId == seriesId).ToList();

    public IReadOnlyList<Figurine> GetFigurinesByTagId(int tagId) =>
        _figurineTags.Items
            .Where(link => link.TagId == tagId)
            .Select(link => GetFigurineById(link.FigurineId))
            .OfType<Figurine>()
            .ToList();

    public IReadOnlyList<CollectionTag> GetTagsByFigurineId(int figurineId) =>
        _figurineTags.Items
            .Where(link => link.FigurineId == figurineId)
            .Select(link => GetTagById(link.TagId))
            .OfType<CollectionTag>()
            .ToList();
}
