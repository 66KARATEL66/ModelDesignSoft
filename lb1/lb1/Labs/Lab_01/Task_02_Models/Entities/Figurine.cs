using lb1.Data;

namespace lb1.Models;

public class Figurine : IEntity
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int Price { get; set; } // uah
    public int AnimeSeriesId { get; set; }
    public AnimeSeries? AnimeSeries { get; set; }
    public FigurineSpecification? Specification { get; set; }
    public List<CollectionTag> Tags { get; } = new();
}
