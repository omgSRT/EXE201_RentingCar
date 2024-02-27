using BusinessObjects.Models;
using RentingCarDAO.DTO;

namespace RentingCarServices.ServiceInterface
{
    public interface IAccountService
    {
        public Account GetAccountByEmail(string email);

        public bool DeleteAccountByEmail(string email);

        public void CreateAccount(string email, string username, string password, string phone);

        public List<Account> GetAllAccounts();

        string SignIn(string email, string password);

        public bool UpdateAccount(int id, NewProfile newProfile);

        public Account GetAccountById(int id);

        public AccountProfileDTO GetAccountProfileById(int id);
        IEnumerable<ImagesLicenseCard> GetImagesLicenseCard();
        ImagesLicenseCard? GetLicenseImageById(long id);
        bool AddLicenseImage(ImagesLicenseCard image);
        bool UpdateLicenseImage(ImagesLicenseCard image);
        bool RemoveLicenseImage(ImagesLicenseCard image);

    }
}
