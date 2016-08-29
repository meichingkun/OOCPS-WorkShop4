using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workshop4
{
    public class Account
    {
        private string accountNumber;
        private Customer accountHolder;
        protected double balance;

        public Account(string number, Customer holder, double bal)
        {
            accountNumber = number;
            accountHolder = holder;
            balance = bal;
        }

        public Account():this("000-000-000", new Customer(), 0) { }

        public string AccountNumber
        {
            get
            {
                return accountNumber;
            }
        }

        public Customer AccountHolder
        {
            get
            {
                return accountHolder;
            }
            set
            {
                accountHolder = value;
            }
        }

        public double Balance
        {
            get
            {
                return balance;
            }
            protected set
            {
                balance = value;
            }
        }

        public void Deposit(double amount)
        {
            balance = balance + amount;
        }

        public bool Withdraw(double amount)
        {
            if(amount > balance)
            {
                balance = balance - amount;
                return (true);
            }else
            {
                Console.WriteLine("Cannot withdraw");
                return (false);
            }
        }

        public void TransferTo(double amount, Account another)
        {
            if (Withdraw(amount))
            {
                another.Deposit(amount);
            }
        }

        public double CalculateInterest()
        {
            return (Balance * 1 / 100);
        }

        public void CreditInterest()
        {
            Deposit(CalculateInterest());
        }

        public string Show()
        {
            string m = String.Format("[BankAccount: accountNumber = {0}, accountHolder = {1}, balance ={2}]", AccountNumber, AccountHolder.Show(), Balance);
            return (m);
        }
        
    }

    public class CurrentAccount : Account
    {
        public CurrentAccount(string number, Customer holder, double bal): base(number, holder, bal) { }

        public new double CalculateInterest()
        {
            return (Balance * 0.25 / 100);
        }

        public new string Show()
        {
            string m = String.Format("[CurrentAccount: accountNumber = {0}, accountHolder = {1}, balance = {2}]", AccountNumber, AccountHolder.Show(), Balance);
            return (m);
        }
    }

    public class OverdraftAccount: Account
    {
        private static double interestRate = 0.25;
        private static double overdraftInterest = 6;

        public OverdraftAccount(string number, Customer holder, double bal):base(number, holder, bal) { }

        public new bool Withdraw(double amount)
        {
            balance = balance - amount;
            return (true);
        }

        public new double CalculateInterest()
        {
            return ((Balance > 0) ?
                (Balance * interestRate / 100) :
                (Balance * overdraftInterest / 100));
        }

        public new string Show()
        {
            string m = String.Format("[OverdraftAccount: accountNumber = {0}, accountHolder = {1}, balance = {2}]", AccountNumber, AccountHolder.Show(), Balance);
            return (m);
        }
    }

    public class Customer
    {
        private string name;
        private string address;
        private string passport;
        private DateTime dateOfBirth;

        public Customer(string name, string address,string passport, DateTime dob):this(name, address, passport)
        {
            this.dateOfBirth = dob;
        }

        public Customer(string name, string address, string passport, int age):this(name, address, passport)
        {
            this.dateOfBirth = new DateTime(DateTime.Now.Year - age, 1, 1);
        }

        public Customer():this("ThisName", "ThisAddress","ThisPassport", new DateTime(1980, 1, 1)) { }

        public Customer(string name, string address, string passport)
        {
            this.name = name;
            this.address = address;
            this.passport = passport;
        }

        public string Name
        {
            get { return name; }
        }
        public string Address
        {
            get { return address; }
            set { address = value; }
        }
        public string Passport
        {
            get { return passport; }
            set { passport = value; }
        }
        public int Age
        {
            get
            {
                return DateTime.Now.Year - dateOfBirth.Year;
            }
        }

        public string Show()
        {
            string m = String.Format("[Customer: name = {0}, address = {1}, passport = {2}, age = {3}]", Name, Address, Passport, Age);
            return (m);
        }

    }

    public class App
    { 
        public static void Main(string[] args)
        {
            Customer cus1 = new Customer("Tan Ah Kow", "2 Rich Street", "P123123", 20);
            Customer cus2 = new Workshop4.Customer("Kim May Mee", "89 Gold Road", "P334412", 60);
            Account a1 = new Account("S0000223", cus1, 2000);
            Console.WriteLine(a1.CalculateInterest());
            a1.CreditInterest();
            Console.WriteLine(a1.Show());
            
            OverdraftAccount a2 = new OverdraftAccount("01230124", cus1, 2000);
            Console.WriteLine(a2.CalculateInterest());
            CurrentAccount a3 = new CurrentAccount("c1230125", cus2, 2000);
            Console.WriteLine(a3.CalculateInterest());
        }
    }
}
