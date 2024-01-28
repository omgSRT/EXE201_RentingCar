using BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentingCarServices.ServiceInterface
{
    public interface IAccountService
    {
        public Account GetAccountByEmail(string email);

        public bool DeleteAccountByEmail(string email);

        public void CreateAccount(string email, string username, string password, string phone);

        public List<Account> GetAllAccounts();

        string SignIn(string email, string password);

    }
}
