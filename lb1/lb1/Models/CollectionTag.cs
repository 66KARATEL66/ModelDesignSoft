namespace lb1.Models;

public sealed class CollectionTag
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public List<Figurine> Figurines { get; } = new();
}
