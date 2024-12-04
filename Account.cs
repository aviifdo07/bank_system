using System;

namespace MyApp
{
    public class Account
    {
        //Private Fields
        private decimal _balance;
        private string _name;

        //Constructor
        public Account(string name, decimal balance)
        {
            _name = name;
            _balance = balance;
        }

        //This line defines a read-only property Name that returns the account name.
        //It uses an expression-bodied member to directly return the _name field.
        public string Name => _name;

        public bool Deposit(decimal amount)
        {
            if (amount <= 0)
            {
                Console.WriteLine("Deposit amount must be positive.");
                return false;
            }

            _balance += amount;
            return true;
        }

        public bool Withdraw(decimal amount)
        {
            if (amount <= 0 || amount > _balance)
            {
                Console.WriteLine("Insufficient funds or invalid amount.");
                return false;
            }

            _balance -= amount;
            return true;
        }

        public void Print()
        {
            Console.WriteLine($"Account Name: {_name}, Balance: {_balance:C}");
        }

        //abstraction is to hide complex implementation details from
        //the user and expose only the necessary components to interact with the system.

        //Encapsulation: The private fields _balance and _name are not accessible directly outside the class,
        //only through methods or read-only properties (e.g., Name).
    }
}
