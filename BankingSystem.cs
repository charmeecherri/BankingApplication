using System;
using System.Collections.Generic;

namespace BankingApp
{
    public class BankingSystem
    {
        private List<User> users = new List<User>();
        private User loggedInUser = null;

        public void Run()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Welcome to the Console Banking Application");
                Console.WriteLine("1. Register");
                Console.WriteLine("2. Login");
                Console.WriteLine("3. Exit");
                Console.Write("Select an option: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Register();
                        break;
                    case "2":
                        Login();
                        if (loggedInUser != null)
                        {
                            ShowUserMenu();
                        }
                        break;
                    case "3":
                        return;
                    default:
                        Console.WriteLine("Invalid option. Press any key to try again.");
                        Console.ReadKey();
                        break;
                }
            }
        }

        private void Register()
        {
            Console.Write("Enter username: ");
            string username = Console.ReadLine();
            Console.Write("Enter password: ");
            string password = Console.ReadLine();

            users.Add(new User(username, password));
            Console.WriteLine("Registration successful! Press any key to return to the menu.");
            Console.ReadKey();
        }

        private void Login()
        {
            Console.Write("Enter username: ");
            string username = Console.ReadLine();
            Console.Write("Enter password: ");
            string password = Console.ReadLine();

            foreach (var user in users)
            {
                if (user.Username == username && user.Password == password)
                {
                    loggedInUser = user;
                    Console.WriteLine("Login successful! Press any key to continue.");
                    Console.ReadKey();
                    return;
                }
            }
            Console.WriteLine("Invalid credentials. Press any key to try again.");
            Console.ReadKey();
        }

        private void ShowUserMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Banking Menu:");
                Console.WriteLine("1. Open Account");
                Console.WriteLine("2. Balance Check");
                Console.WriteLine("3. Withdraw ");
                Console.WriteLine("4. Generate Statement ");
                Console.WriteLine("5. Calculate Interest ");
                Console.WriteLine("6.Logout");
                Console.Write("Select an option: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        OpenAccount();
                        break;
                    case "2":
                        BalanceCheck();
                        break;
                    case "3":
                        Withdraw();
                       
                        break;
                    case "4":
                        GenerateStatement();
                        break;
                    case "5":
                        CalculateInterest();
                        break;

                    case "6":
                        loggedInUser = null;
                        return;
                       
                    default:
                        Console.WriteLine("Invalid option. Press any key to try again.");
                        Console.ReadKey();
                        break;
                }
            }
        }

        private void OpenAccount()
        {
            Console.Write("Enter account holder's name: ");
            string accountHolderName = Console.ReadLine();
            Console.Write("Enter account type (savings/checking): ");
            string accountType = Console.ReadLine();
            Console.Write("Enter initial deposit amount: ");
            double initialDeposit;
            while (!double.TryParse(Console.ReadLine(), out initialDeposit) || initialDeposit < 0)
            {
                Console.Write("Invalid amount. Please enter a valid initial deposit: ");
            }

            Account newAccount = new Account(accountHolderName, accountType, initialDeposit);
            loggedInUser.Accounts.Add(newAccount);

            Console.WriteLine("Account opened successfully! Press any key to return to the menu.");
            Console.ReadKey();
        }

        private void BalanceCheck()
        {
            Console.Write("Enter account holder's name: ");
            string accountHolderName = Console.ReadLine();

            // Get accounts matching the entered holder's name
            List<Account> matchingAccounts = loggedInUser.GetAccountsByHolderName(accountHolderName);

            if (matchingAccounts.Count == 0)
            {
                Console.WriteLine("No accounts found for this account holder.");
                return;
            }

            Console.WriteLine("Select an account by number:");
            for (int i = 0; i < matchingAccounts.Count; i++)
            {
                Console.WriteLine($"{i + 1}. Account {matchingAccounts[i].AccountNumber} - {matchingAccounts[i].AccountType}");
            }

            int choice;
            while (!int.TryParse(Console.ReadLine(), out choice) || choice < 1 || choice > matchingAccounts.Count)
            {
                Console.Write("Invalid selection. Please choose a valid account number: ");
            }

            // Display the balance for the selected account
            Account selectedAccount = matchingAccounts[choice - 1];
            Console.WriteLine($"The balance for account {selectedAccount.AccountNumber} ({selectedAccount.AccountType}) is: {selectedAccount.Balance}");
            Console.WriteLine("Press any key to return to the menu.");
            Console.ReadKey();
        }

        private void GenerateStatement()
        {
            Account account = SelectAccount();
            if (account != null)
            {
                Console.WriteLine("Transaction History:");
                foreach (var transaction in account.Transactions)
                {
                    Console.WriteLine($"{transaction.Date}: {transaction.Type} - {transaction.Amount}");
                }
                Console.WriteLine("Press any key to return.");
                Console.ReadKey();
            }
        }

        private void CalculateInterest()
        {
            Account account = SelectAccount();
            if (account != null)
            {
                if (account.AccountType.ToLower() == "savings")
                {
                    double interest = account.Balance * 0.04; // Assuming 4% interest rate
                    account.Deposit(interest);
                    Console.WriteLine($"Interest of {interest} added to balance.");
                }
                else
                {
                    Console.WriteLine("Interest calculation only applies to savings accounts.");
                }
                Console.WriteLine("Press any key to return.");
                Console.ReadKey();
            }
        }

        private Account SelectAccount()
        {
            if (loggedInUser.Accounts.Count == 0)
            {
                Console.WriteLine("No accounts found. Please open an account first.");
                Console.ReadKey();
                return null;
            }

            Console.WriteLine("Select an account by number:");
            for (int i = 0; i < loggedInUser.Accounts.Count; i++)
            {
                Console.WriteLine($"{i + 1}. Account {loggedInUser.Accounts[i].AccountNumber} - {loggedInUser.Accounts[i].AccountType}");
            }

            int selection;
            if (int.TryParse(Console.ReadLine(), out selection) && selection > 0 && selection <= loggedInUser.Accounts.Count)
            {
                return loggedInUser.Accounts[selection - 1];
            }

            Console.WriteLine("Invalid selection. Press any key to return.");
            Console.ReadKey();
            return null;
        }
        private void Withdraw()
        {
            Account account = SelectAccount();
            if (account == null)
            {
                return; // Exit if no account is selected
            }

            Console.Write("Enter the amount to withdraw: ");
            double amount;
            while (!double.TryParse(Console.ReadLine(), out amount) || amount <= 0)
            {
                Console.Write("Invalid amount. Please enter a valid amount to withdraw: ");
            }

            if (account.Balance >= amount)
            {
                account.Withdraw(amount);  // Assumes Account class has a Withdraw method
                Console.WriteLine($"Withdrawal of {amount} successful. New balance: {account.Balance}");
            }
            else
            {
                Console.WriteLine("Insufficient funds. Withdrawal cannot be processed.");
            }

            Console.WriteLine("Press any key to return to the menu.");
            Console.ReadKey();
        }

    }
}
