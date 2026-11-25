namespace lb1.Models;

public sealed class AnimeSeries
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Studio { get; set; } = string.Empty;
    public List<Figurine> Figurines { get; } = new();
}
