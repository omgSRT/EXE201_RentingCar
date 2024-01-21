using BusinessObjects.Models;
using Microsoft.AspNetCore.Mvc;
using RentingCarServices.ServiceInterface;

namespace RentingCarAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly ILogger<AccountController> _logger;
        private readonly IAccountService _accountService;

        public AccountController(ILogger<AccountController> logger, IAccountService accountService)
        {
            _logger = logger;
            _accountService = accountService;
        }

        [HttpDelete(Name = "Delete Account")]
        public IActionResult Delete(string email)
        {
            bool checkDelete = _accountService.DeleteAccountByEmail(email);
            if (checkDelete == true)
            {
                return Ok("Delete Successfully");
            }
            return BadRequest();
        }

        [HttpPost(Name = "Create Account")]
        public IActionResult CreateAccount(string email, string username , string password, int? phone)
        {
            _accountService.CreateAccount(email, username,  password, phone);
            return Ok("Add Successfully");
        }

        [HttpGet(Name = "Search Account By Email")]
        public Account GetAccountByEmail(string email)
        {
            Account account = _accountService.GetAccountByEmail(email);
            return account;
        }
    }
}