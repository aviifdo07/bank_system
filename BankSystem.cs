using System;
using System.Collections.Generic;

namespace MyApp
{
    //This defines an enumeration MenuOption
    //Enumerations make code more readable and less prone to errors.
    //An enumeration that defines possible menu options for
    //the banking system: Withdraw, Deposit, Transfer, Print, and Quit.
    enum MenuOption
    {
        Withdraw = 1,
        Deposit,
        Transfer,
        Print,
        AddAccount,
        PrintTransactionHistory,
        RollbackTransaction,
        Quit
    }

    public class BankSystem
    {
        private Bank _bank = new Bank();

        public void Run()
        {
            bool keepRunning = true;

            while (keepRunning)
            {
                MenuOption userChoice = ReadUserOption();

                switch (userChoice)
                {
                    case MenuOption.Withdraw:
                        DoWithdraw();
                        break;

                    case MenuOption.Deposit:
                        DoDeposit();
                        break;

                    case MenuOption.Transfer:
                        DoTransfer();
                        break;

                    case MenuOption.Print:
                        DoPrint();
                        break;

                    case MenuOption.AddAccount:
                        DoAddAccount();
                        break;

                    case MenuOption.PrintTransactionHistory:
                        _bank.PrintTransactionHistory();
                        break;

                    case MenuOption.RollbackTransaction:
                        DoRollback();
                        break;

                    case MenuOption.Quit:
                        keepRunning = false;
                        Console.WriteLine("Thank you for using our banking system. Goodbye!");
                        break;

                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            }
        }

        private MenuOption ReadUserOption()
        {
            MenuOption selectedOption;
            bool isValidOption = false;

            do
            {
                Console.WriteLine("\nMenu:");
                Console.WriteLine("1. Withdraw");
                Console.WriteLine("2. Deposit");
                Console.WriteLine("3. Transfer");
                Console.WriteLine("4. Print Account Details");
                Console.WriteLine("5. Add New Account");
                Console.WriteLine("6. Print Transaction History");
                Console.WriteLine("7. Rollback Transaction");
                Console.WriteLine("8. Quit");

                Console.Write("Please choose an option (1-8): ");
                string input = Console.ReadLine();

                if (Enum.TryParse(input, out selectedOption) && Enum.IsDefined(typeof(MenuOption), selectedOption))
                {
                    isValidOption = true;
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a number between 1 and 8.");
                }

            } while (!isValidOption);

            return selectedOption;
        }

        private void DoAddAccount()
        {
            Console.Write("Enter account holder's name: ");
            string? name = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(name))
            {
                Console.WriteLine("Invalid account name. Please enter a valid name.");
                return;
            }

            Console.Write("Enter starting balance: ");
            if (decimal.TryParse(Console.ReadLine(), out decimal balance))
            {
                Account newAccount = new Account(name, balance);
                _bank.AddAccount(newAccount);
                Console.WriteLine("Account successfully created.");
            }
            else
            {
                Console.WriteLine("Invalid balance. Please enter a valid number.");
            }
        }

        private void DoWithdraw()
        {
            var account = FindAccount();
            if (account == null)
            {
                Console.WriteLine("Account not found.");
                return; // Prevents further execution if account is null
            }

            Console.Write("Enter the amount to withdraw: ");
            if (decimal.TryParse(Console.ReadLine(), out decimal amount))
            {
                var transaction = new WithdrawTransaction(account, amount);
                try
                {
                    _bank.ExecuteTransaction(transaction);
                    transaction.Print();
                }
                catch (InvalidOperationException ex)
                {
                    Console.WriteLine($"Transaction failed: {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine("Invalid amount. Please enter a valid number.");
            }
        }

        private void DoDeposit()
        {
            var account = FindAccount();
            if (account == null)
            {
                Console.WriteLine("Account not found.");
                return; // Prevents further execution if account is null
            }

            Console.Write("Enter the amount to deposit: ");
            if (decimal.TryParse(Console.ReadLine(), out decimal amount))
            {
                var transaction = new DepositTransaction(account, amount);
                try
                {
                    _bank.ExecuteTransaction(transaction);
                    transaction.Print();
                }
                catch (InvalidOperationException ex)
                {
                    Console.WriteLine($"Transaction failed: {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine("Invalid amount. Please enter a valid number.");
            }
        }

        private void DoTransfer()
        {
            var fromAccount = FindAccount();
            if (fromAccount == null)
            {
                Console.WriteLine("From Account not found.");
                return; // Prevents further execution if fromAccount is null
            }

            var toAccount = FindAccount();
            if (toAccount == null)
            {
                Console.WriteLine("To Account not found.");
                return; // Prevents further execution if toAccount is null
            }

            Console.Write("Enter the amount to transfer: ");
            if (decimal.TryParse(Console.ReadLine(), out decimal amount))
            {
                var transaction = new TransferTransaction(fromAccount, toAccount, amount);
                try
                {
                    _bank.ExecuteTransaction(transaction);
                    transaction.Print();
                }
                catch (InvalidOperationException ex)
                {
                    Console.WriteLine($"Transfer failed: {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine("Invalid amount. Please enter a valid number.");
            }
        }

        private void DoPrint()
        {
            var account = FindAccount();
            if (account == null)
            {
                Console.WriteLine("Account not found.");
                return; // Prevents further execution if account is null
            }
            account.Print();
        }

        private void DoRollback()
        {
            _bank.PrintTransactionHistory();

            Console.Write("Enter the transaction number to rollback: ");
            if (int.TryParse(Console.ReadLine(), out int index))
            {
                if (index > 0 && index <= _bank.Transactions.Count) // Access through the property
                {
                    var transaction = _bank.Transactions[index - 1]; // Access through the property
                    try
                    {
                        _bank.RollbackTransaction(transaction);
                        Console.WriteLine("Transaction rolled back successfully.");
                    }
                    catch (InvalidOperationException ex)
                    {
                        Console.WriteLine($"Rollback failed: {ex.Message}");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid transaction number.");
                }
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a valid transaction number.");
            }
        }

        private Account? FindAccount()
        {
            Console.Write("Enter account holder's name: ");
            string? name = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(name))
            {
                Console.WriteLine("Invalid account name. Please enter a valid name.");
                return null;
            }

            var account = _bank.GetAccount(name);
            if (account == null)
            {
                Console.WriteLine("Account not found.");
            }
            return account;
        }
    }

    internal class Program
    {
        static void Main(string[] args) // Fixed the missing static keyword
        {
            BankSystem bankSystem = new BankSystem();
            bankSystem.Run();
        }
    }
    //Polymorphism is a key principle that allows objects to be treated as instances of their parent class.
    //This provides the ability to call methods on objects without knowing their specific types at compile time.
}
