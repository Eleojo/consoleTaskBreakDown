using BankApp.Classes;
using BankApp.Database;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Security.Principal;

namespace BankApp.Methods
{
    public  class Transactions
    {
        public  float accountBalance;
        List<User> users = BankDataBase.GetAllUsers(); // Get all users from the database
        List<Account> accounts = BankDataBase.GetAllAccounts(); // Get all accounts from the database

        public  void Withdraw(float withdrawAmount, Guid userId)
        {
            bool accountFound = false;

            foreach (Account account in accounts)
            {
                if (account.userId.Equals(userId))
                {
                    accountFound = true;

                    if (withdrawAmount > account.accountBalance)
                    {
                        Console.WriteLine("Insufficient Funds");
                    }
                    else
                    {
                        account.accountBalance -= withdrawAmount;
                        BankDataBase.UpdateAccounts(accounts);
                        Console.WriteLine($"Success !! You have withdrawn {withdrawAmount} and your new balance is {account.accountBalance} ");
                        Console.WriteLine("Press Enter key to return to main menu");
                    }

                    break; // Exit the loop once the account is found and processed
                }
            }

            if (!accountFound)
            {
                Console.WriteLine("Sorry, you do not have an account with us.");
            }
        }

        public  void DepositMoney()
        {
            bool accountFound = false;

            Console.WriteLine("Enter your identifier (first name for now):");
            string identifier = Console.ReadLine();
            TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
            identifier = textInfo.ToTitleCase(identifier);

            //var users = BankDataBase.GetAllUsers(); // Get all users from the database
            //var accounts = BankDataBase.GetAllAccounts(); // Get all accounts from the database

            foreach (User user in users)
            {
                if (user.FirstName.Equals(identifier, StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine("Enter amount to deposit:");
                    if (float.TryParse(Console.ReadLine(), out float depositAmount))
                    {
                        foreach (Account account in accounts)
                        {
                            if (account.userId.Equals(user.Id))
                            {
                                
                                account.accountBalance += depositAmount;
                                accountFound = true;
                                Console.WriteLine($"Success! You have deposited {depositAmount} and your new balance is {account.accountBalance}.");
                                break; // Exit the inner loop once the account is found and updated
                            }
                        }

                        if (accountFound)
                        {
                            // Update the accounts collection in the database
                            BankDataBase.UpdateAccounts(accounts);
                            //Console.WriteLine($"Success! You have deposited {depositAmount} and your new balance is {account.accountBalance}.");
                            Console.WriteLine("Press Enter key to return to the main menu.");
                        }
                        else
                        {
                            Console.WriteLine("Account not found.");
                        }
                        break; // Exit the outer loop once the account is found and updated
                    }
                    else
                    {
                        Console.WriteLine("Invalid amount entered.");
                        break; // Exit if an invalid amount is entered
                    }
                }
            }

            if (!accountFound)
            {
                Console.WriteLine("Sorry, you do not have an account with us.");
            }
        }


        public void Transfer(float amount)
        {
            Guid senderUserId = Guid.Empty;
            Guid receiverUserId = Guid.Empty;

            Console.WriteLine("Enter sender identifier (first name):");
            string senderIdentifier = Console.ReadLine();
            Console.WriteLine("Enter receiver identifier (first name):");
            string receiverIdentifier = Console.ReadLine();

            
            foreach (User user in users)
            {
                if (user.FirstName.Equals(senderIdentifier, StringComparison.OrdinalIgnoreCase))
                {
                    senderUserId = user.Id;
                }
                if (user.FirstName.Equals(receiverIdentifier, StringComparison.OrdinalIgnoreCase))
                {
                    receiverUserId = user.Id;
                }
            }

            // Check if both users were found
            if (senderUserId == Guid.Empty || receiverUserId == Guid.Empty)
            {
                Console.WriteLine($"One or both users not found. Please make sure both sender and receiver are registered. Sender ID: {senderUserId}, Receiver ID: {receiverUserId}");
                return;
            }

            Account senderAccount = null;
            Account receiverAccount = null;

            // Find sender and receiver accounts
            foreach (Account account in accounts)
            {
                if (account.userId.Equals(senderUserId))
                {
                    senderAccount = account;
                }
                if (account.userId.Equals(receiverUserId))
                {
                    receiverAccount = account;
                }
            }

            // Check if both accounts were found
            if (senderAccount == null || receiverAccount == null)
            {
                Console.WriteLine("One or both accounts not found. Please make sure both sender and receiver have accounts.");
                return;
            }

            // Check if sender has sufficient funds
            if (senderAccount.accountBalance < amount)
            {
                Console.WriteLine("Insufficient funds in the sender's account.");
                return;
            }

            // Perform the transfer
            senderAccount.accountBalance -= amount;
            receiverAccount.accountBalance += amount;
            //UpdateRowSource the account in database
            BankDataBase.UpdateAccounts(accounts);

            Console.WriteLine($"Transfer successful! {amount} has been transferred from {senderIdentifier} to {receiverIdentifier}.");
            Console.WriteLine($"Sender's new balance: {senderAccount.accountBalance}");
            Console.WriteLine($"Receiver's new balance: {receiverAccount.accountBalance}");
        }
    }
}
