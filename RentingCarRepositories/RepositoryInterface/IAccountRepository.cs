﻿using BusinessObjects.Models;
using RentingCarDAO.DTO;

namespace RentingCarRepositories.RepositoryInterface
{
    public interface IAccountRepository
    {
        public Account GetAccountByEmail(string email);

        public Account GetAccountById(int id);

        public AccountProfileDTO GetAccountProfileById(int id);

        public bool DeleteAccount(Account account);

        public void CreateAccount(Account account);

        public List<Account> GetAllAccounts();

        public bool UpdateAccount(Account newAccount);
        IEnumerable<ImagesLicenseCard> GetImagesLicenseCard();
        ImagesLicenseCard? GetLicenseImageById(long id);
        bool AddLicenseImage(ImagesLicenseCard image);
        bool UpdateLicenseImage(ImagesLicenseCard image);
        bool RemoveLicenseImage(ImagesLicenseCard image);

    }
}
