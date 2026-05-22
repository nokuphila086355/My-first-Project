using System;
using System.ComponentModel.Design;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

namespace BankAccount
{
    class BankAccount
    {
        private string AccountHolder;
        private string AccountNumber;
        private string Pin;
        private decimal Balance;

        public BankAccount(string _AccountHolder, string _AccountNumber, string _Pin, decimal _Balance)
        {
            AccountHolder = _AccountHolder;
            AccountNumber = _AccountNumber;
            Pin = _Pin;
            Balance = _Balance;
        }

        public decimal balance => Balance;
        public string accountHolder => AccountHolder;
        public string accountNumber => AccountNumber;


        public bool ValidatePin(string _Pin)
        {
            if (_Pin == Pin) return true;
            else return false;

        }

        public void Deposit(decimal _Amount)
        {
            if (_Amount <= 0)
            {
                throw new ArithmeticException("ivalid _Amount - Amount should be >0");
            }

            Balance += _Amount;
        }
        public void Withdraw(decimal _Amount)
        {
            if (_Amount <= 0)

            {
                throw new ArithmeticException("ivalid _Amount - Amount should be >0");
            }

            if (_Amount > Balance)

            {
                throw new ArithmeticException("Insufficient Funds");
            }
            Balance -= _Amount;
        }

    }

    class Program
    {
        public static void Main(string[] args)
        {
            BankAccount account = new BankAccount("John Doe", "123456789", "1234", 1000);
            try
            {
                Console.WriteLine("Enter your PIN:");
                string inputPin = Console.ReadLine();
                if (account.ValidatePin(inputPin))
                {
                    Console.WriteLine("PIN is correct. You can proceed with transactions.");
                    account.Deposit(500);
                    Console.WriteLine($"After deposit, balance: {account.balance}");
                    account.Withdraw(200);
                    Console.WriteLine(inputPin);

                }
                else
                {
                    Console.WriteLine("Invalid PIN. Access denied.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

    }
}

