using System;

namespace MyApp
{
    public class WithdrawTransaction : Transaction
    {
        private Account _account;
        private bool _success;

        //Constructor
        //Constructor for the WithdrawTransaction class, which initializes the transaction with
        //the account from which to withdraw and the amount.
        public WithdrawTransaction(Account account, decimal amount) : base(amount)
        {
            _account = account;
        }

        //This method overrides the abstract Execute method from the Transaction base class.
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
            _success = _account.Withdraw(_amount);
            _dateStamp = DateTime.Now;
        }

        //This method is responsible for reversing (rolling back) the transaction, if possible.
        public override void Rollback()
        {
            if (!_executed || _rolledBack)
                throw new InvalidOperationException("Cannot rollback. Either the transaction was not executed or it was already rolled back.");

            if (!Success)
                throw new InvalidOperationException("Cannot rollback a failed transaction.");

            _reversed = _account.Deposit(_amount); // Undo by depositing back
            _rolledBack = true;
            _dateStamp = DateTime.Now;
        }

        public override void Print()
        {
            Console.WriteLine(_success ?
                $"Withdrawal of {_amount:C} from {_account.Name} successful on {_dateStamp}." :
                "Withdrawal transaction failed.");
        }

        //This is an override of the Success property from the Transaction base class.
        public override bool Success => _success;

       
        //Polymorphism is a key principle that allows objects to be treated as instances of their parent class.

    }
}
