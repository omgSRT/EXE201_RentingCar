using BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentingCarRepositories.RepositoryInterface
{
    public interface IAccountRepository
    {
        public Account GetAccountByEmail(string email);

        public bool DeleteAccount(Account account);

        public void CreateAccount(Account account);

        public List<Account> GetAllAccounts();

    }
}
