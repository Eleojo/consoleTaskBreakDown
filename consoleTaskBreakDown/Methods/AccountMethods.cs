using BankApp.Classes;
using BankApp.Database;
using LiteDB;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using ConsoleTableExt;
using System.Linq;

//using ConsoleTables;

namespace BankApp.Methods
{
    internal class AccountMethods
    {

        public void UpdateMyAccountProfile()
        {

        }
        public static void OpenAccount(User newUser, string accountType)
        {
            string accountNumber = GenerateRandomAccountNumber();  // Generate the 10-digit account number
            float initialAccountBalance = 10;

            bool userFound = false;
            Guid id = Guid.NewGuid();



            foreach (User user in BankDataBase.GetAllUsers())
            {
                if (user.Id.Equals(newUser.Id))
                {
                    userFound = true;

                    // Check if the user already has an account
                    foreach (Account account in BankDataBase.GetAllAccounts())
                    {
                        if (account.userId.Equals(newUser.Id))
                        {
                            Console.WriteLine("You already have an account with us.");
                            return;
                        }
                    }

                    // If no account exists, create a new one
                    Account newAccount = new Account( id, newUser.Id, initialAccountBalance, accountNumber, accountType);
                    //BankDataBase.AccountsDatabase.Add(newAccount);
                    BankDataBase.Create(newAccount);
                    Console.WriteLine($"\nAccount created successfully for user {user.FirstName} with account number {accountNumber}.");
                    break;
                }
            }

            if (!userFound)
            {
                Console.WriteLine("Please register first before trying to create an account.");
            }
        }

        private static string GenerateRandomAccountNumber()
        {
            Random random = new Random();
            // Generate two 5-digit numbers and concatenate them
            int part1 = random.Next(10000, 100000); // Generates a 5-digit number
            int part2 = random.Next(10000, 100000); // Generates another 5-digit number
            return part1.ToString() + part2.ToString(); // Concatenate them to form a 10-digit number
        }





        //private static string GenerateRandomAccountNumber()
        //{
        //    Random random = new Random();
        //    return random.Next(1000000000, 2000000000).ToString();
        //}

        //public static void DisplayAccountInfo()
        //{


        //    Console.WriteLine("Enter account identifier (first name for now):");
        //    string identifier = Console.ReadLine();
        //    TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
        //    identifier = textInfo.ToTitleCase(identifier);

        //    foreach (User user in BankDataBase.GetAllUsers())
        //    {
        //        if (user.FirstName.Equals(identifier))
        //        {
        //            foreach (Account account in BankDataBase.GetAllAccounts())
        //            {
        //                //if (account.userId.Equals(user.Id))
        //               // {
        //                    //AccountMethods.DisplayAccountInfo($"{user.FirstName} {user.LastName}", user.Id);


        //                   // foreach (Account account in BankDataBase.GetAllAccounts())
        //                    {
        //                        if (account.userId.Equals(user.Id))
        //                        {
        //                            Console.WriteLine("| FULL NAME            | ACCOUNT NUMBER | ACCOUNT TYPE | AMOUNT BAL |");
        //                            Console.WriteLine("|----------------------|----------------|--------------|------------|");
        //                            Console.WriteLine($"|{user.FirstName} {user.LastName} |{account.accountNumber}      |{account.accountType}       | {account.accountBalance}         | {account.userId}");
        //                        }

        //                    }

        //                //}
        //            }

        //        }
        //    }
        //}







        public static void DisplayAccountInfo()
        {
            Console.WriteLine("Enter account identifier (first name for now):");
            string identifier = Console.ReadLine();
            TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
            identifier = textInfo.ToTitleCase(identifier);
            int index = 0;
            int length = BankDataBase.GetAllUsers().Count;
            List<AccountDisplay> accountDisplays = new List<AccountDisplay>();

            foreach (User user in BankDataBase.GetAllUsers())
            {
                if (user.FirstName.Equals(identifier))
                {
                    foreach (Account account in BankDataBase.GetAllAccounts())
                    {
                        if (user.Id.Equals(account.userId))
                        {
                            accountDisplays.Add(new AccountDisplay
                            {
                                FullName = $"{user.FirstName} {user.LastName}",
                                AccountNumber = account.accountNumber,
                                AccountType = account.accountType,
                                AccountBalance = account.accountBalance,
                                UserId = account.userId
                            });
                        }
                    }
                }
                if (!user.FirstName.Equals(identifier) && index == length - 1)
                {
                    Console.WriteLine("User not found in database");
                }
                index++;
            }

            ConsoleTableBuilder
                .From(accountDisplays)
                .WithFormat(ConsoleTableBuilderFormat.Alternative)
                .ExportAndWriteLine();
        }


        public static void ClearDatabase()
        {
            using (var db = new LiteDatabase(@"MyBankDatabase.db"))
            {
                // Get all collection names
                var collectionNames = db.GetCollectionNames();

                // Drop each collection
                foreach (var name in collectionNames)
                {
                    db.DropCollection(name);
                }
            }
        }


        public void showAllAccounts(List<Account> account)
        {
            ConsoleTableBuilder
                .From(account)
                .WithFormat(ConsoleTableBuilderFormat.Alternative)
                .ExportAndWriteLine();
        }
        private class AccountDisplay
            {
                public string FullName { get; set; }
                public string AccountNumber { get; set; }
                public string AccountType { get; set; }
                public float AccountBalance { get; set; }
                public Guid UserId { get; set; }
        }
    }
}










