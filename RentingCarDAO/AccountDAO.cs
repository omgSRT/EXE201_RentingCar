using BusinessObjects.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentingCarDAO
{
    public class AccountDAO
    {
        private static AccountDAO instance = null;
        private static exe201Context db = null;

        public AccountDAO()
        {
            db = new exe201Context();
        }
        public static AccountDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new AccountDAO();
                }
                return instance;
            }
        }

        public List<Account> GetAccounts()
        {
            return db.Accounts.ToList();
        }

        public Account GetAccountByEmail(string email)
        {
            try
            {
                return db.Accounts.Where(m => m.Email.Equals(email))
                    .Include("Role")
                    .Include("Status")
                    .FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw new Exception();
            }
        }

        public bool DeleteAccount(Account account)
        {
            try
            {
                var accountToDelete = db.Accounts.FirstOrDefault(m => m.Email == account.Email);
                if (accountToDelete != null)
                {
                    db.Accounts.Remove(accountToDelete);
                    db.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("Delete Fail");
            }

        }

        public void CreateAccount(Account account)
        {
            try
            {
                var checkExist = db.Accounts.FirstOrDefault(m => m.Email == account.Email);
                if (checkExist == null)
                {
                    db.Accounts.Add(account);
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Create Fail");
            }


        }

        
    }
}
