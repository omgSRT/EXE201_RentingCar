using BusinessObjects.Models;
using RentingCarRepositories.RepositoryInterface;
using RentingCarServices.ServiceInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentingCarServices.Service
{
    public class AccountService : IAccountService
    {
        public readonly IAccountRepository _accountRepository;

        public AccountService(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public void CreateAccount(string email, string username,  string password, int? phone)
        {
            if (email != null && username!= null && password!= null && phone!=null)
            {
                
                    Account account = new Account
                    {
                        Email = email,
                        UserName = username,
                        Password = password,
                        Phone = phone??0,
                        RoleId = 1,
                        StatusId = 1,
                        
                    };
                    _accountRepository.CreateAccount(account);
                

            }
        }

        public bool DeleteAccountByEmail(string email)
        {
            Account account = _accountRepository.GetAccountByEmail(email);
            if (account == null)
            {
                return false;
            }
            bool checkDelete = _accountRepository.DeleteAccount(account);
            return checkDelete;
        }

        public Account GetAccountByEmail(string email)
        {
            Account account = _accountRepository.GetAccountByEmail(email);
            if (account != null)
            {
                return account;
            }
            return null;
        }
    }
}
