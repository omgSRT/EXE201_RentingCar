using BusinessObjects.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RentingCarDAO.DTO;
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

        [HttpDelete("DeleteAccount",Name = "Delete Account")]
        public IActionResult Delete(string email)
        {
            bool checkDelete = _accountService.DeleteAccountByEmail(email);
            if (checkDelete == true)
            {
                return Ok("Delete Successfully");
            }
            return BadRequest();
        }

        [HttpPost("CreateAccount",Name = "CreateAccount")]
        public IActionResult CreateAccount(string email, string username , string password, string confirmPassword)
        {
            try
            {
                _accountService.CreateAccount(email, username, password, confirmPassword);
                return Ok("Add Successfully");
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        [HttpGet("SearchByEmail/{email}",Name = "Search Account By Email")]
        public Account GetAccountByEmail(string email)
        {        
            return _accountService.GetAccountByEmail(email);
        }

        [HttpGet("SearchAllAccount", Name = "Search All Account")]
        //[Authorize(Roles = "USER")]
        public List<Account> GetAllAccount()
        {
            
            return _accountService.GetAllAccounts(); 
        }

        [HttpPost("SignIn",Name = "Sign In")]
        public IActionResult SignIn(string email, string password)
        {
            var result = _accountService.SignIn(email, password);
            if (result != null)
            {              
                return Ok(result);
            }
            return Unauthorized();
        }

        [HttpPost("UpdateProfile/{id}", Name = "Update Current User Profile")]
        public IActionResult UpdateProfile(int id, NewProfile newProfile)
        {
            try
            {
                var result = _accountService.UpdateAccount(id, newProfile);
                if (result != null)
                {
                    return Ok(result);
                }
                return Unauthorized();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetAccountProfile/{id}", Name = "Get Current User Profile")]
        public AccountProfileDTO ViewCurrentUserProfile(int id)
        {           
             return _accountService.GetAccountProfileById(id);                                                   
        }
    }
}