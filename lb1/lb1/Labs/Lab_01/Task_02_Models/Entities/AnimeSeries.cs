using lb1.Data;

namespace lb1.Models;

public class AnimeSeries : IEntity
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Studio { get; set; } = string.Empty;
    public List<Figurine> Figurines { get; } = new();
}
