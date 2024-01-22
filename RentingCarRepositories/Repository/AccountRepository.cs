using BusinessObjects.Models;
using RentingCarDAO;
using RentingCarRepositories.RepositoryInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentingCarRepositories.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private AccountDAO _accountDAO;
        public AccountRepository()
        {
            _accountDAO = new AccountDAO();
        }
        public void CreateAccount(Account account)
        {
            _accountDAO.CreateAccount(account);
        }

        public bool DeleteAccount(Account account)
        {
            return _accountDAO.DeleteAccount(account);
        }

        public Account GetAccountByEmail(string email)
        {
            return _accountDAO.GetAccountByEmail(email);
        }

        public List<Account> GetAllAccounts()
        {
            return _accountDAO.GetAccounts();
        }
    }
}
