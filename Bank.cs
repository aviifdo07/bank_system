using System;
using System.Collections.Generic;

//The Bank class is designed to manage a collection of multiple bank accounts, allowing the user to
//perform various operations like adding accounts, finding accounts, and executing different types of transactions (deposit, withdraw, and transfer)
namespace MyApp
{
    public class Bank
    {
        //A list to store multiple bank accounts
        private List<Account> _accounts = new List<Account>();
        private List<Transaction> _transactions = new List<Transaction>();

        // Method to add an account to the bank
        public void AddAccount(Account account)
        {
            _accounts.Add(account);
        }

        // Method to find an account by name
        public Account? GetAccount(string name)
        {
            return _accounts.Find(account => account.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        }

        //This method executes a transaction by calling its Execute method and then adds it to the _transactions list,
        //regardless of whether the transaction was successful.
        public void ExecuteTransaction(Transaction transaction)
        {
            transaction.Execute();
            _transactions.Add(transaction); // Store every transaction, successful or not
        }

        //This method attempts to roll back a previously executed transaction
        public void RollbackTransaction(Transaction transaction)
        {
            if (transaction.RolledBack)
                throw new InvalidOperationException("Transaction has already been rolled back.");

            if (!transaction.Success)
                throw new InvalidOperationException("Cannot rollback a failed transaction.");

            transaction.Rollback();
        }

        //This method prints all the transactions stored in _transactions
        public void PrintTransactionHistory()
        {
            for (int i = 0; i < _transactions.Count; i++)
            {
                Console.Write($"{i + 1}. ");
                _transactions[i].Print();
            }
        }

        //This is a public property that provides read-only access to the _transactions list.
        //It uses an expression-bodied member to return the list directly.
        public List<Transaction> Transactions => _transactions;
    }
}
