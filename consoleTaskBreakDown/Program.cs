//using BankApp.Classes;
//using BankApp.Database;
//using BankApp.Methods;
//using System.Data.Common;
//using System.Globalization;

//namespace BankApp
//{
//    internal class Program
//    {
//        static void Main(string[] args)
//        {
//            Console.WriteLine("WELCOME TO MY BANK APP\n\n");
//            Console.WriteLine("What would you like to do today\n");


//            Console.WriteLine("Press 1 to Register and Open an Account.");
//            Console.WriteLine("Press 2 to Withdraw money.");
//            Console.WriteLine("Press 3 to Deposit money.");
//            Console.WriteLine("Press 4 to Display Account info.");
//            Console.WriteLine("Press 5 to Exit.");
//            string userInput = Console.ReadLine();

//            if (string.IsNullOrEmpty(userInput))
//            {
//                Console.WriteLine("Please input 1 or 2");
//            }
//            else if (userInput == "1")
//            {

//                // Create a textInfo object to define the culture capitilization rule
//                TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;

//                string email;
//                string firstName;
//                string lastName;
//                string password;
//                //string accountPassword;
//                //string accountNumber;
//                //string accountType;

//                bool validEmail;
//                bool validName;
//                bool validPassword;

//                // Register the User
//                //var accountMethod = new AccountMethods();

//                // Prompt till validations are true
//                do
//                {
//                    Console.WriteLine("Enter your FirstName");
//                    firstName = Console.ReadLine();

//                    // Capitalize the first letter of each word in the input string (*Sanitize)
//                    firstName = textInfo.ToTitleCase(firstName);

//                    // Perform Validation
//                    validName = Validations.IsValidName(firstName);
//                    if (!validName)
//                    {
//                        Console.WriteLine("Name cannot be Empty or have Digits, Please try again.");
//                    }
//                }
//                while (!validName);

//                do
//                {
//                    Console.WriteLine("Enter your LastName");
//                    lastName = Console.ReadLine();


//                    // Capitalize the first letter of each word in the input string
//                    lastName = textInfo.ToTitleCase(lastName);
//                    validName = Validations.IsValidName(lastName);
//                    if (!validName)
//                    {
//                        Console.WriteLine("Name cannot be Empty or have Digits, Please try again.");
//                    }

//                }
//                while (!validName);



//                do
//                {
//                    Console.WriteLine("Enter your email");
//                    email = Console.ReadLine();
//                    validEmail = Validations.IsValidEmail(email);

//                    if (!validEmail)
//                    {
//                        Console.WriteLine("Incorrect email address, Please try again.");
//                    }
//                }
//                while (!validEmail);


//                do
//                {

//                    Console.WriteLine("Enter your password");
//                    password = Console.ReadLine();
//                    validPassword = Validations.IsValidPassword(password);

//                    if (!validPassword)
//                    {
//                        Console.WriteLine("Try again!Ensure Password contains at least 6 characters, one upper case letter, one digit and one special character. ");
//                    }
//                }
//                while (!validPassword);


//                UserMethods userMethods = new UserMethods();

//                //Console.WriteLine($"There are {BankDataBase.UserDatabase.Count} users in userdatabase");
//                //Console.WriteLine($"There are {BankDataBase.AccountsDatabase.Count} account in accountsdatabase");

//                userMethods.Register(firstName, lastName, email, password);
//                AccountMethods.OpenAccount(BankDataBase.UserDatabase[0].Id, "Savings", "Rice");
//                Console.WriteLine("Congratulations. You have been registered  and successfully opened a new account with us. Press Enter to perform another transaction.");

//                //Console.WriteLine($"There are now {BankDataBase.UserDatabase.Count} users in userdatabase");
//                //Console.WriteLine($"There are now {BankDataBase.AccountsDatabase.Count} account in accountsdatabase");

//                foreach (User user in BankDataBase.UserDatabase)
//                {
//                    Console.WriteLine($"{user.FirstName} {user.LastName} has an account with us.");
//                }
//                Console.ReadKey();
//                Console.Clear();
//                Main(args);
//            }
//            else if (userInput == "2")
//            {
//                Console.WriteLine("Enter your identifier(firstname for now)");
//                string Identifier = Console.ReadLine();
//                foreach (User user in BankDataBase.UserDatabase)
//                {
//                    if (user.FirstName.Equals(Identifier))
//                    {
//                        Transactions transactions = new Transactions();
//                        transactions.Withdraw(23, user.Id);
//                    }

//                    //foreach(Account account in BankDataBase.AccountsDatabase)
//                    //{
//                    //    if(user.FirstName.Equals(Identifier) && user.Id.Equals(account.userId))
//                    //    {
//                    //        account
//                    //    }

//                    //}
//                    Console.ReadKey();
//                    Console.Clear();
//                    Main(args);
//                }
//            }
//            else if (userInput == "3")
//            {
//                Console.WriteLine("Enter your identifier(firstname for now)");
//                string Identifier = Console.ReadLine();
//                foreach (User user in BankDataBase.UserDatabase)
//                {
//                    if (user.FirstName.Equals(Identifier))
//                    {
//                        Transactions transactions = new Transactions();

//                        int depositAmount = int.Parse(Console.ReadLine());
//                        transactions.Deposit(depositAmount, user.Id);
//                    }
//                    else
//                    {
//                        Console.WriteLine("Please register first before you can perform this task.");
//                    }

//                }
//                Console.ReadKey();
//                Console.Clear();
//                Main(args);

//            }

//            else if(userInput == "4")
//            {
//                Console.WriteLine("Enter account identifier (first name for now)");
//                string Identifier = Console.ReadLine();

//                foreach(User user in BankDataBase.UserDatabase)
//                {

//                    foreach (Account account in BankDataBase.AccountsDatabase)
//                    {
//                        if (account.userId.Equals(user.Id))
//                        {
//                            AccountMethods.DisplayAccountInfo(user.Id);
//                        }
//                    }
//                }

//            }

//            else if (userInput == "5")
//            {

//                Console.WriteLine("Thank you for banking with us.");
//                Environment.Exit(0);
//            }
//        }



//    }
//}


using BankApp.Classes;
using BankApp.Database;
using BankApp.Methods;
using System.Globalization;
using System.Runtime;

namespace BankApp
{
    internal class Program
    {

        static void Main(string[] args)
        {
            Program program = new Program();
            program.Run();
        }

        private void Run()
        {

            Console.WriteLine("WELCOME TO MY BANK APP\n\n");

            while (true)
            {
                ShowMenu();

                string userInput = Console.ReadLine();

                if (string.IsNullOrEmpty(userInput))
                {
                    Console.WriteLine("Please input a valid option.");
                }
                else if (userInput == "1")
                {
                    RegisterAndOpenAccount();
                }
                else if (userInput == "2")
                {
                    WithdrawMoney();
                }
                else if (userInput == "3")
                {
                    Transactions transactions = new Transactions();
                    transactions.DepositMoney();
                }
                else if (userInput == "4")
                {
                    //AccountMethods.ClearDatabase();
                    AccountMethods.DisplayAccountInfo();
                }
                else if(userInput == "5")
                {
                    Transactions transactions = new Transactions();
                    Console.WriteLine("Enter amount to transfer");
                    string readAmount = Console.ReadLine();
                    float amount = float.Parse(readAmount);
                    transactions.Transfer(amount);
                }
                //else if(userInput == "6")
                else if (userInput == "6")
                {
                    Console.WriteLine("Thank you for banking with us.");
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid option, please try again.");
                }
            }
        }

        private void ShowMenu()
        {
            Console.WriteLine("\nWhat would you like to do today\n");
            Console.WriteLine("Press 1 to Register and Open an Account.");
            Console.WriteLine("Press 2 to Withdraw money.");
            Console.WriteLine("Press 3 to Deposit money.");
            Console.WriteLine("Press 4 to Display Account info.");
            Console.WriteLine("Press 5 to transfer.");
            Console.WriteLine("Press 6 to Exit.");
        }

        private void RegisterAndOpenAccount()
        {
            
            string email;
            string firstName;
            string lastName;
            string password;

            bool validEmail;
            bool validName;
            bool validPassword;

            do
            {
                Console.WriteLine("Enter your FirstName");
                firstName = Console.ReadLine();
                //TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
                //firstName = textInfo.ToTitleCase(firstName);
                validName = Validations.IsValidName(firstName);
                if (!validName)
                {
                    Console.WriteLine("Name cannot be Empty or have Digits, Please try again.");
                }
            }
            while (!validName);

            do
            {
                Console.WriteLine("Enter your LastName");
                lastName = Console.ReadLine();
                validName = Validations.IsValidName(lastName);
                if (!validName)
                {
                    Console.WriteLine("Name cannot be Empty or have Digits, Please try again.");
                }
            }
            while (!validName);

            do
            {
                Console.WriteLine("Enter your email");
                email = Console.ReadLine();
                validEmail = Validations.IsValidEmail(email);
                if (!validEmail)
                {
                    Console.WriteLine("Incorrect email address, Please try again.");
                }
            }
            while (!validEmail);

            do
            {
                Console.WriteLine("Enter your password");
                password = Console.ReadLine();
                validPassword = Validations.IsValidPassword(password);
                if (!validPassword)
                {
                    Console.WriteLine("Try again! Ensure Password contains at least 6 characters, one upper case letter, one digit and one special character. ");
                }
            }
            while (!validPassword);

            UserMethods userMethods = new UserMethods();
            TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
            firstName = textInfo.ToTitleCase(firstName);
            lastName = textInfo.ToTitleCase(lastName);

            userMethods.Register(firstName, lastName, email, password);
            User user = BankDataBase.GetAllUsers()[^1]; // takes the last recently registered userId
            
            Console.WriteLine("Enter the account type you want to open");
            string accountType = Console.ReadLine();
            

            AccountMethods.OpenAccount(user, accountType );
            //Console.WriteLine("Congratulations. You have been registered and successfully opened a new account with us.");
        }

        private void WithdrawMoney()
        {
            Console.WriteLine("Enter your identifier (firstname for now):");

            string identifier = Console.ReadLine();
            TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
            identifier = textInfo.ToTitleCase(identifier);

            foreach (User user in BankDataBase.GetAllUsers())
            {
                if (user.FirstName.Equals(identifier))
                {
                    Transactions transactions = new Transactions();
                    Console.WriteLine("Enter amount to withdraw:");
                    int amount = int.Parse(Console.ReadLine());
                    transactions.Withdraw(amount, user.Id);
                    return;
                }
            }

            Console.WriteLine("User not found. Please register first.");
        }

       


        

    }
}
