using BusinessObjects.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RentingCarAPI.ViewModel;
using RentingCarDAO.DTO;
using RentingCarServices.Service;
using RentingCarServices.ServiceInterface;
using System.Linq.Expressions;

namespace Payment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MembershipController : ControllerBase
    {
        private readonly IAccountService _accountService;
        public MembershipController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(ResponseVMWithEntity<Account>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseVM), StatusCodes.Status404NotFound)]

        public IActionResult UpdateRoleMembership(int id)
        {

            try
            {
                var account = _accountService.GetAccountById(id);
                if (account == null)
                {
                    return NotFound(new ResponseVM
                    {
                        Message = "Cannot Find This Membership",
                        Errors = new string[] { "There's No Membership With ID " + id }
                    });
                }
                if (account.RoleId != 2)
                {
                    return BadRequest(new ResponseVM
                    {
                        Message = "Cannot Update Role Of Membership",
                        Errors = new string[] { "Current Role Is Admin Or Premium User" }
                    });
                }
                    var oldAccount = _accountService.GetAccountById(id);
                    oldAccount.RoleId = 3;

                var check = _accountService.UpdateAccount(oldAccount);
                if (!check)
                {
                    return BadRequest(new ResponseVMWithEntity<Account>
                    {
                        Message = "Cannot Update Role",
                        Errors = new string[] { "Error Handling Update From Database" },
                        Entity = oldAccount
                    });
                }
                return Ok(new ResponseVMWithEntity<Account>
                    {
                        Message = "Update Role of Membership Successfully",
                        Entity = oldAccount,
                    });

            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseVM
                {
                    Message = "Cannot Update Role Of Membership",
                    Errors = new string[] { ex.Message }
                });
            }
        }
    }
}

