using lb1.Data;

namespace lb1.Models;

public class CollectionTag : IEntity
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public List<Figurine> Figurines { get; } = new();
}
