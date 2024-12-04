using System;

namespace MyApp
{
    //This class inherits from the abstract Transaction class and implements
    //the specific behavior for depositing funds into an account.
    public class DepositTransaction : Transaction
    {
        //Private fields
        private Account _account;
        private bool _success;

        //Constructor
        //This is the constructor for the DepositTransaction class, which initializes the transaction with the account where
        //the deposit will be made and the amount to deposit.
        public DepositTransaction(Account account, decimal amount) : base(amount)
        {
            _account = account;
        }

        //This method overrides the abstract Execute method from the Transaction base
        //class and contains the logic to perform the deposit.
        public override void Execute()
        {
            if (_executed)
            {
                throw new InvalidOperationException("Transaction already executed.");
            }

            _executed = true;
            if (_account.Withdraw(_amount))
            {
                _success = true;
            }
            else
            {
                _success = false;
            }
            _success = _account.Deposit(_amount);
            _dateStamp = DateTime.Now;
        }

        public override void Rollback()
        {
            if (!_executed || _rolledBack)
                throw new InvalidOperationException("Cannot rollback. Either the transaction was not executed or it was already rolled back.");

            if (!Success)
                throw new InvalidOperationException("Cannot rollback a failed transaction.");

            _reversed = _account.Withdraw(_amount); // Undo by withdrawing back
            _rolledBack = true;
            _dateStamp = DateTime.Now;
        }

        public override void Print()
        {
            Console.WriteLine(_success ?
                $"Deposit of {_amount:C} to {_account.Name} successful on {_dateStamp}." : //// Include the date and time in the print statement
                "Deposit transaction failed.");
        }

        public override bool Success => _success;

        ////Inheritance allows one class (derived class) to inherit properties, fields, and methods from another class (base class).
        //In this case, all three classes, inherit from the base class Transaction.
    }
}
