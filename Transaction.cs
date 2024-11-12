public class Transaction
{
    public static int TransactionCounter = 1;
    public int TransactionId { get; }
    public DateTime Date { get; }
    public string Type { get; }
    public double Amount { get; }

    public Transaction(string type, double amount,DateTime? date=null)
    {
        TransactionId = TransactionCounter++;
        Date = date ?? DateTime.Now;
        Type = type;
        Amount = amount;
    }
}
