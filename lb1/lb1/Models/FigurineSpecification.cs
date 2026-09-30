using lb1.Data;

namespace lb1.Models;

public class FigurineSpecification : IEntity
{
    public int Id { get; set; }
    public int FigurineId { get; set; }
    public decimal HeightCm { get; set; }
    public string Material { get; set; } = string.Empty;
    public bool LimitedEdition { get; set; }
    public Figurine? Figurine { get; set; }
}
