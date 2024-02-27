using BusinessObjects.Models;
using RentingCarDAO;
using RentingCarDAO.DTO;
using RentingCarRepositories.RepositoryInterface;

namespace RentingCarRepositories.Repository
{
    public class AccountRepository : IAccountRepository
    {

        private AccountDAO _accountDAO;
        public AccountRepository()
        {

            _accountDAO = new AccountDAO();
        }

        public bool AddLicenseImage(ImagesLicenseCard image)
        {
            return _accountDAO.AddLicenseImage(image);
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

        public Account GetAccountById(int id)
        {
            return _accountDAO.GetAccountById(id);
        }

        public AccountProfileDTO GetAccountProfileById(int id)
        {
            return _accountDAO.GetAccountProfileById(id);
        }

        public List<Account> GetAllAccounts()
        {
            return _accountDAO.GetAccounts();
        }

        public IEnumerable<ImagesLicenseCard> GetImagesLicenseCard()
        {
            return _accountDAO.GetImagesLicenseCard();
        }

        public ImagesLicenseCard? GetLicenseImageById(long id)
        {
            return _accountDAO.GetLicenseImageById(id);
        }

        public bool RemoveLicenseImage(ImagesLicenseCard image)
        {
            return _accountDAO.RemoveLicenseImage(image);
        }

        public bool UpdateAccount(Account newAccount)
        {
            return _accountDAO.UpdateProfile(newAccount);
        }

        public bool UpdateLicenseImage(ImagesLicenseCard image)
        {
            return _accountDAO.UpdateLicenseImage(image);
        }
    }
}
