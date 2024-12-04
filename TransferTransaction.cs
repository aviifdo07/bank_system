using System;

namespace MyApp
{
    public class TransferTransaction : Transaction
    {
        //Each transaction class encapsulates the logic for handling specific banking operations,
        //keeping the details of how these operations are carried out hidden from the rest of the system.
        //Private fields
        //This stores the account from which money will be transferred (the source account).
        private Account _fromAccount;
        //This stores the account to which money will be transferred (the destination account).
        private Account _toAccount;
        //Holds an instance of WithdrawTransaction to handle the withdrawal part of the transfer.
        private WithdrawTransaction _withdrawTransaction;
        //Holds an instance of DepositTransaction to handle the deposit part of the transfer.
        private DepositTransaction _depositTransaction;


        //Constructor
        //This constructor initializes the transfer with the source account, destination account, and the amount to transfer.
        public TransferTransaction(Account fromAccount, Account toAccount, decimal amount) : base(amount)
        {
            //Initializes the source account (_fromAccount) where the money will be withdrawn from.
            _fromAccount = fromAccount;
            _toAccount = toAccount;
            _withdrawTransaction = new WithdrawTransaction(fromAccount, amount);
            _depositTransaction = new DepositTransaction(toAccount, amount);
        }

        //Datetime is used to record the exact time when the transaction is executed, rolled back, or printed. 
        public override void Execute()
        {
            _withdrawTransaction.Execute();
            if (_withdrawTransaction.Success)
            {
                _depositTransaction.Execute();
                _executed = true;
            }
            _dateStamp = DateTime.Now;
        }

        public override void Rollback()
        {
            if (!_executed || _rolledBack)
                throw new InvalidOperationException("Cannot rollback.");

            _depositTransaction.Rollback();
            _withdrawTransaction.Rollback();
            _rolledBack = true;
            _dateStamp = DateTime.Now;
        }

        public override void Print()
        {
            Console.WriteLine($"Transfer executed on {_dateStamp}:");
            _withdrawTransaction.Print();
            _depositTransaction.Print();
        }

        public override bool Success => _withdrawTransaction.Success && _depositTransaction.Success;
        //Inheritance allows one class (derived class) to inherit properties, fields, and methods from another class (base class).
        //In this case, all three classes, inherit from the base class Transaction.
    }
}
