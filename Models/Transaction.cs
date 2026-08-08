using System;

namespace PrimeCapitalBank.Models;

public class Transaction
{
    public Guid TransactionId { get; set; } = Guid.NewGuid();
    public Guid RelatedTransactionId { get; set; }
    public DateTime Date { get ; set; } = DateTime.Now;
    public string Type { get ; set; } = string.Empty;
    public decimal Amount { get ; set; }
    public string Description { get; set; } = string.Empty;
    public bool IsCredit { get; set; }
}