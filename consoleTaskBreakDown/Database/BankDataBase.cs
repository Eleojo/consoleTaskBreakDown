using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankApp.Classes;
using LiteDB;


namespace BankApp.Database
{
    public static class BankDataBase
    {
        //public static List<User> UserDatabase = new List<User>();
        //public static List<Account> AccountsDatabase = new List<Account>();


        public static void Create<T>(T obj)
        {
            using (var db = new LiteDatabase(@"MyBankDatabase.db"))
            {
                var users = db.GetCollection<User>("users");
                var accounts = db.GetCollection<Account>("accounts");

                if(obj is  User user)
                {
                    users.Insert(user);
                }
                else if(obj is Account account) 
                {
                    accounts.Insert(account);
                }
                else
                {
                    Console.WriteLine("Unknown Object Type");
                }
            }
        }

        public static List<User> GetAllUsers()
        {
            using (var db = new LiteDatabase(@"MyBankDatabase.db"))
            {
                var users = db.GetCollection<User>("users");
                return users.FindAll().ToList();
            }
        }

        public static List<Account> GetAllAccounts()
        {
            using (var db = new LiteDatabase(@"MyBankDatabase.db"))
            {
                var accounts = db.GetCollection<Account>("accounts");
                return accounts.FindAll().ToList();
            }
        }

        public static void UpdateAccounts(List<Account> accounts)
        {
            using (var db = new LiteDatabase(@"MyBankDatabase.db"))
            {
                var accountsCollection = db.GetCollection<Account>("accounts");
                foreach (Account account in accounts)
                {
                    Console.WriteLine(account.userId)
;                   accountsCollection.Update(account);
                }
            }
        }


    }




}
