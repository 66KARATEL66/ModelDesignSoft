namespace lb1.Data;

public class DBItem<T> where T : IEntity
{
    private readonly List<T> _items = new List<T>();

    public IReadOnlyList<T> Items => _items;

    public void Add(T entity)
    {
        _items.Add(entity);
    }
}
