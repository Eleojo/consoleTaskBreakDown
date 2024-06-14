using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankApp.Classes;
using BankApp.Database;

namespace BankApp.Methods
{
    public  class UserMethods
    {

        public void Register(string firstName, string lastName, string email, string password)
        {
            var id = Guid.NewGuid();
            //var validEmail = new Email();

            User newUser = new User(id, firstName, lastName, email, password);
            //BankDataBase.UserDatabase.Add(newUser);
            BankDataBase.Create(newUser);

        }
    }
}


