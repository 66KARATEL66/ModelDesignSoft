using lb1.Data;

namespace lb1.Models;

public enum PaymentStatus
{
    Pending,
    Completed,
    Failed
}

public class Payment : IEntity
{
    public int Id { get; set; }
    public int OrderId { get; set; }
    public decimal Amount { get; set; }
    public PaymentStatus Status { get; set; } = PaymentStatus.Pending;
    public DateTime? PaidAt { get; set; }
}
