namespace lb1.Models;

public sealed class Figurine
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int Price { get; set; }
    public int AnimeSeriesId { get; set; }
    public AnimeSeries? AnimeSeries { get; set; }
    public FigurineSpecification? Specification { get; set; }
    public List<CollectionTag> Tags { get; } = new();
}
