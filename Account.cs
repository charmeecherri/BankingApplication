public class Account
{
    private static int accountCounter = 1001;
    public int AccountNumber { get; }
    public string AccountHolderName { get; }
    public string AccountType { get; }
    public double Balance { get; private set; }
    public List<Transaction> Transactions { get; } = new List<Transaction>();

    public Account(string accountHolderName, string accountType, double initialDeposit)
    {
        AccountNumber = accountCounter++;
        AccountHolderName = accountHolderName;
        AccountType = accountType;
        Balance = initialDeposit;
    }

    public void Deposit(double amount)
    {
        Balance += amount;
        Transactions.Add(new Transaction("Deposit", amount));
    }

    //public void Withdraw(double amount)
    //{
    //    if (amount > Balance)
    //    {
    //        Console.WriteLine("Insufficient funds.");
    //    }
    //    else
    //    {
    //        Balance -= amount;
    //        Transactions.Add(new Transaction("Withdrawal", amount));
    //    }
    //}
    public void Withdraw(double amount)
    {
        if (amount <= Balance)
        {
            Balance -= amount;
            //Transactions.Add(new Transaction(DateTime.Now, "Withdraw", amount,));
            Transactions.Add(new Transaction("Withdraw", amount, DateTime.Now));
            Console.WriteLine($"Withdrawal of {amount} completed. New balance: {Balance}");
        }
        else
        {
            Console.WriteLine("Insufficient funds for withdrawal.");
        }
    }

}
