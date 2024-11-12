using BankingApp;

public class User
{
    public string Username { get; }
    public string Password { get; }
    public List<Account> Accounts { get; } = new List<Account>();

    public User(string username, string password)
    {
        Username = username;
        Password = password;
    }
    public List<Account> GetAccountsByHolderName(string holderName)
    {
        return Accounts.Where(account => account.AccountHolderName.Equals(holderName, StringComparison.OrdinalIgnoreCase)).ToList();
    }

    public void OpenAccount(string accountHolderName, string accountType, double initialDeposit)
    {
        Accounts.Add(new Account(accountHolderName, accountType, initialDeposit));
    }
}

