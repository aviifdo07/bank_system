using System;

namespace MyApp
{
    //Constructor
    public abstract class Transaction
    {
        //Act as like Encapsulation the class hides the internal fields from external access
        //but allows derived classes to access and modify them
        //Holds the monetary value of the transaction.
        protected decimal _amount;
        //Records the date and time the transaction was created.
        protected DateTime _dateStamp;
        //Indicates whether the transaction has been executed.
        protected bool _executed;
        //Indicates whether the transaction has been reversed.
        protected bool _reversed;
        // Indicates whether the transaction has been rolled back.
        protected bool _rolledBack;

        //Inheritance is Used here
        public Transaction(decimal amount)
        {
            
            _amount = amount;
            _executed = false;
            _reversed = false;
            _rolledBack = false;
            _dateStamp = DateTime.Now;
        }

        //Method to execute the transaction.
        public abstract void Execute();
        //Method to rollback the transaction.
        public abstract void Rollback();
        //Method to print details about the transaction.
        public abstract void Print();
        //Property that indicates whether the transaction was successful.
        public abstract bool Success { get; }

        //Properties
        public DateTime DateStamp => _dateStamp;

        public bool RolledBack => _rolledBack;

        //Polymorphism is a key principle that allows objects to be treated as instances of their parent class.


        //an abstract class serves as a blueprint for other classes. It cannot be instantiated on its own, meaning you
        //cannot create objects directly from an abstract class.
    }
}
