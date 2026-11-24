using lb1.Data;

namespace lb1.Models;

public class OrderItem : IEntity
{
    public int Id { get; set; }
    public int OrderId { get; set; }
    public int FigurineId { get; set; }
    public Figurine? Figurine { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
}
