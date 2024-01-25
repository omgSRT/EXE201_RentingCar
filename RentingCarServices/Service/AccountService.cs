using BusinessObjects.Models;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RentingCarRepositories.RepositoryInterface;
using RentingCarServices.ServiceInterface;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace RentingCarServices.Service
{
    public class AccountService : IAccountService
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<Account> _userManager;
        private readonly SignInManager<Account> _signInManager;
        public readonly IAccountRepository _accountRepository;

        public AccountService(IAccountRepository accountRepository, UserManager<Account> userManager,
            SignInManager<Account> signInManager, IConfiguration configuration)
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
            this._configuration = configuration;
            _accountRepository = accountRepository;
        }

        public void CreateAccount(string email, string username,  string password, int? phone)
        {
            if (email != null && username!= null && password!= null && phone!=null)
            {

                // generate a 128-bit salt using a cryptographically strong random sequence of nonzero values
                byte[] salt = new byte[128 / 8];
                using (var rngCsp = new RNGCryptoServiceProvider())
                {
                    rngCsp.GetNonZeroBytes(salt);
                }
                // derive a 256-bit subkey (use HMACSHA256 with 100,000 iterations)
                string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                    password: password,
                    salt: salt,
                    prf: KeyDerivationPrf.HMACSHA512,
                    iterationCount: 100000,
                    numBytesRequested: 512 / 8));
                Account account = new Account
                    {
                        Email = email,
                        UserName = username,
                        Password = hashed,
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

        public List<Account> GetAllAccounts()
        {
            return _accountRepository.GetAllAccounts();
        }

        public async Task<string> SignIn(string email, string password)
        {
            var result = await _signInManager.PasswordSignInAsync(email, password, false, false);
            if (!result.Succeeded)
            {
                return string.Empty;
            }
            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };
            var authenkey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddMinutes(30),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authenkey, SecurityAlgorithms.HmacSha512)
                ); 
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
