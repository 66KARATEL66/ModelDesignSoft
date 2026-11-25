namespace lb1.Models;

public enum PaymentStatus
{
    Pending,
    Completed,
    Failed
}

public sealed class Payment
{
    public int Id { get; set; }
    public int OrderId { get; set; }
    public decimal Amount { get; set; }
    public PaymentStatus Status { get; set; } = PaymentStatus.Pending;
    public DateTime? PaidAt { get; set; }
}
