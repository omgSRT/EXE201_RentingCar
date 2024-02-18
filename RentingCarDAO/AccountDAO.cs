using BusinessObjects.Models;
using Microsoft.EntityFrameworkCore;
using RentingCarDAO.DTO;
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
            return db.Accounts
                .Include(account => account.Role)
                .Include(account => account.Status)
                .ToList();
        }

        public Account GetAccountByEmail(string email)
        {
            try
            {
                Account account = db.Accounts.Where(m => m.Email.Equals(email))
                    .Include("Role")
                    .Include("Status")
                    .FirstOrDefault();
                return account;
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }

        public Account GetAccountById(int id)
        {
            try
            {
                return db.Accounts.Where(m => m.AccountId == id)
                    .Include("Role")
                    .Include("Status")
                    .FirstOrDefault();
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }

        public AccountProfileDTO GetAccountProfileById(int id)
        {
            try
            {
                var account = db.Accounts.Where(m => m.AccountId == id)
                    .Include("Role")
                    .Include("Status")
                    .Select(m => new AccountProfileDTO
                    {                        
                        UserName = m.UserName,
                        Email = m.Email,
                        Address = m.Address,
                        Country = m.Country,
                        Phone = m.Phone,
                    })
                    .FirstOrDefault();
                return account;
            }
            catch (Exception)
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
            catch (Exception)
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
            catch (Exception e)
            {
                var innerException = e.InnerException;
                throw new Exception(e.Message, innerException);
            }


        }

        public bool UpdateProfile (Account newAccount)
        {
            try
            {
                db.Update(newAccount);
                db.SaveChanges();
                return true;
            }catch (Exception e)
            {
                throw new Exception ("Update fail");
            }
            
        }
    }
}
