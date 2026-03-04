using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project
{
    class Bank
    {
        private string bank_name;
        private Account[] all_accounts;
        private int accountCount;

        public Bank(string name)
        {
            bank_name = name;
            all_accounts = new Account[100];
            accountCount = 0;
            Console.WriteLine($"{bank_name} Branch 1122");
        }

        public void AddAccount(Account new_account)
        {
            if (accountCount < all_accounts.Length)
            {
                all_accounts[accountCount] = new_account;
                accountCount++;
                Console.WriteLine($"Account added to {bank_name}");
            }
            else
            {
                Console.WriteLine("Bank is full! Cannot add more accounts.");
            }
        }

        public Account FindAccount(string account_number)
        {
            for (int i = 0; i < accountCount; i++)
            {
                if (all_accounts[i].GetAccountNumber() == account_number)
                {
                    return all_accounts[i];
                }
            }
            return null;
        }

        public void ShowAllAccounts()
        {
            if (accountCount == 0)
            {
                Console.WriteLine("No accounts in the bank!");
                return;
            }

            Console.WriteLine($"\n All Accounts in {bank_name}:");
            Console.WriteLine("════════════════════════════════");

            for (int i = 0; i < accountCount; i++)
            {
                Account account = all_accounts[i];
                Console.WriteLine($"{i + 1}. {account.GetAccountHolderName()} - Acc#: {account.GetAccountNumber()} - Balance: ${account.GetBalance()}");
            }

            Console.WriteLine("════════════════════════════════\n");
        }

        public double GetTotalMoney()
        {
            double total_money = 0;

            for (int i = 0; i < accountCount; i++)
            {
                total_money = total_money + all_accounts[i].GetBalance();
            }

            return total_money;
        }

        public void DisplayBankInfo()
        {
            Console.WriteLine("\nBANK INFORMATION");
            Console.WriteLine("════════════════════════════════");
            Console.WriteLine($"Bank Name: {bank_name}");
            Console.WriteLine($"Total Accounts: {accountCount}");
            Console.WriteLine($"Total Money in Bank: ${GetTotalMoney()}");
            Console.WriteLine("════════════════════════════════\n");
        }
    }
    class Account
    {
        private string account_number;
        private string account_holder_name;
        private double account_balance;

        public Account(string number, string name, double starting_balance)
        {
            account_number = number;
            account_holder_name = name;
            account_balance = starting_balance;

            Console.WriteLine("New account created!");
        }

        public string GetAccountNumber()
        {
            return account_number;
        }

        public string GetAccountHolderName()
        {
            return account_holder_name;
        }

        public double GetBalance()
        {
            return account_balance;
        }

        public void Deposit(double amount)
        {
            if (amount > 0)
            {
                account_balance = account_balance + amount;
                Console.WriteLine($"Deposit successful! Amount: PKR{amount}");
            }
            else
            {
                Console.WriteLine("Amount must be greater than 0!");
            }
        }

        public void Withdraw(double amount)
        {
            if (amount > 0 && amount <= account_balance)
            {
                account_balance = account_balance - amount;
                Console.WriteLine($"Withdrawal successful! Amount: PKR{amount}");
            }
            else if (amount > account_balance)
            {
                Console.WriteLine("Not enough money in your account!");
            }
            else
            {
                Console.WriteLine("Amount must be greater than 0!");
            }
        }

        public void DisplayAccountInfo()
        {
            Console.WriteLine("════════════════════════════════");
            Console.WriteLine($"Account Number: {account_number}");
            Console.WriteLine($"Account Holder: {account_holder_name}");
            Console.WriteLine($"Balance: PKR{account_balance}");
            Console.WriteLine("════════════════════════════════");
        }
    }
    class Menu
    { 
        private Bank my_bank;

        public Menu(Bank bank)
        {
            my_bank = bank;
        }

        public void ShowMainMenu()
        {
            Console.WriteLine("\n╔════════════════════════════════╗");
            Console.WriteLine("║   BANKING MANAGEMENT SYSTEM    ║");
            Console.WriteLine("╠════════════════════════════════╣");
            Console.WriteLine("║ 1. Create New Account          ║");
            Console.WriteLine("║ 2. Deposit Money               ║");
            Console.WriteLine("║ 3. Withdraw Money              ║");
            Console.WriteLine("║ 4. Check Balance               ║");
            Console.WriteLine("║ 5. View All Accounts           ║");
            Console.WriteLine("║ 6. View Bank Information       ║");
            Console.WriteLine("║ 7. Exit                        ║");
            Console.WriteLine("╚════════════════════════════════╝");
        }

        public void HandleUserChoice(int choice)
        {
            switch (choice)
            {
                case 1:
                    {
                        CreateAccount();
                        break;
                    }
                case 2:
                    {
                        DepositMoney();
                        break;
                    }
                case 3:
                    {
                        WithdrawMoney();
                        break;
                    }
                case 4:
                    {
                        CheckBalance();
                        break;
                    }
                case 5:
                    {
                        ViewAllAccounts();
                        break;
                    }
                case 6:
                    {
                        ViewBankInfo();
                        break;
                    }
                case 7:
                    {
                        Console.WriteLine("\nThank you for using our bank! Goodbye!");
                        break;
                    }
                default:
                    {
                        Console.WriteLine("\nInvalid choice! Please try again.");
                        break;
                    }
            }
        }

        private void CreateAccount()
        {
            Console.WriteLine("\n--- Create New Account ---");

            Console.Write("Enter Account Number: ");
            string account_number = Console.ReadLine();

            Console.Write("Enter Account Holder Name: ");
            string account_holder_name = Console.ReadLine();

            Console.Write("Enter Starting Balance: PKR ");
            double starting_balance = double.Parse(Console.ReadLine());

            Account new_account = new Account(account_number, account_holder_name, starting_balance);
            my_bank.AddAccount(new_account);

        }
        private void DepositMoney()
        {
            Console.WriteLine("\n--- Deposit Money ---");

            Console.Write("Enter Account Number: ");
            string account_number = Console.ReadLine();

            Account found_account = my_bank.FindAccount(account_number);

            if (found_account != null)
            {
                Console.Write("Enter Amount to Deposit: PKR ");
                double amount = double.Parse(Console.ReadLine());
                found_account.Deposit(amount);
            }
            else
            {
                Console.WriteLine("Account not found!");
            }
        }

        private void WithdrawMoney()
        {
            Console.WriteLine("\n--- Withdraw Money ---");

            Console.Write("Enter Account Number: ");
            string account_number = Console.ReadLine();

            Account found_account = my_bank.FindAccount(account_number);

            if (found_account != null)
            {
                Console.Write("Enter Amount to Withdraw: $");
                double amount = double.Parse(Console.ReadLine());
                found_account.Withdraw(amount);
            }
            else
            {
                Console.WriteLine("Account not found!");
            }
        }

        private void CheckBalance()
        {
            Console.WriteLine("\n--- Check Balance ---");

            Console.Write("Enter Account Number: ");
            string account_number = Console.ReadLine();

            Account found_account = my_bank.FindAccount(account_number);

            if (found_account != null)
            {
                Console.WriteLine($"\nBalance for {found_account.GetAccountHolderName()}: ${found_account.GetBalance()}");
            }
            else
            {
                Console.WriteLine("Account not found!");
            }
        }

        private void ViewAllAccounts()
        {
            my_bank.ShowAllAccounts();
        }

        private void ViewBankInfo()
        {
            my_bank.DisplayBankInfo();
        }
    }
    class Program
    {
        static void Main(string[] args)
        { 
                Bank my_bank = new Bank("Habib Bank Limited (HBL)");

                Console.WriteLine("\n╔════════════════════════════════╗");
                Console.WriteLine("║        WELCOME TO BANK!        ║");
                Console.WriteLine("╚════════════════════════════════╝");

                Menu bank_menu = new Menu(my_bank);

                bool is_running = true;

                while (is_running)
                {

                    bank_menu.ShowMainMenu();

                    Console.Write("Enter your choice (1-7): ");
                    int input = int.Parse(Console.ReadLine());
                    if (input != 0)
                    { 
                        bank_menu.HandleUserChoice(input);

                        if (input == 7)
                        {
                            is_running = false;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid input. Please enter a number.");
                    }
                }
        }

    }
}