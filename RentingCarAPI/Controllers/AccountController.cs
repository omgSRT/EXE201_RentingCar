using BusinessObjects.Models;
using CloudinaryDotNet.Actions;
using CloudinaryDotNet;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RentingCarAPI.ViewModel;
using RentingCarDAO.DTO;
using RentingCarServices.Service;
using RentingCarServices.ServiceInterface;

namespace RentingCarAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly ILogger<AccountController> _logger;
        private readonly IAccountService _accountService;
        private readonly Cloudinary _cloudinary;

        public AccountController(ILogger<AccountController> logger, IAccountService accountService, Cloudinary cloudinary)
        {
            _logger = logger;
            _accountService = accountService;
            _cloudinary = cloudinary;
        }

        [HttpDelete("DeleteAccount",Name = "Delete Account")]
        [ProducesResponseType(typeof(ResponseVM), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseVM), StatusCodes.Status400BadRequest)]
        public IActionResult Delete(string email)
        {
            bool checkDelete = _accountService.DeleteAccountByEmail(email);
            if (checkDelete == true)
            {
                return Ok(new ResponseVM
                {
                    Message = "Delete Successfully"
                });
            }
            return BadRequest(new ResponseVM
            {
                Message = "Cannot Delete Account With Email " +email,
                Errors = new string[] {"Error While Deleting Account"}
            });
        }

        [HttpPost("CreateAccount",Name = "CreateAccount")]
        [ProducesResponseType(typeof(ResponseVM), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseVM), StatusCodes.Status400BadRequest)]
        public IActionResult CreateAccount(string email, string username , string password, string confirmPassword)
        {
            try
            {
                _accountService.CreateAccount(email, username, password, confirmPassword);
                return Ok(new ResponseVM
                {
                    Message = "Create Successfully"
                });
            } catch (Exception ex)
            {
                return BadRequest(new ResponseVM
                {
                    Message = "Cannot Create Account",
                    Errors = new string[] { "Invalid Input", ex.Message }
                });
            }
            
        }

        [HttpGet("SearchByEmail/{email}",Name = "Search Account By Email")]
        [ProducesResponseType(typeof(ResponseVMWithEntity<BusinessObjects.Models.Account>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseVM), StatusCodes.Status404NotFound)]
        public IActionResult GetAccountByEmail(string email)
        {
            var account = _accountService.GetAccountByEmail(email);
            if(account == null)
            {
                return NotFound(new ResponseVM
                {
                    Message = "There's No Data With Email" + email,
                    Errors = new string[] {"No Data Match With Email" +email}
                });
            }
            return Ok(new ResponseVMWithEntity<BusinessObjects.Models.Account>
            {
                Message = "Found Successfully",
                Entity = account
            });
        }

        [HttpGet("SearchAllAccount", Name = "Search All Account")]
        [ProducesResponseType(typeof(List<BusinessObjects.Models.Account>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseVM), StatusCodes.Status404NotFound)]
        //[Authorize(Roles = "USER")]
        public IActionResult GetAllAccount()
        {
            var accountList = _accountService.GetAllAccounts();
            if (!accountList.Any())
            {
                return NotFound(new ResponseVM
                {
                    Message = "There's No Data",
                    Errors = new string[] { "There's No Data" }
                });
            }
            return Ok(accountList); 
        }

        [HttpPost("SignIn",Name = "Sign In")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseVM), StatusCodes.Status400BadRequest)]
        public IActionResult SignIn(string email, string password)
        {
            var result = _accountService.SignIn(email, password);
            if (result != null)
            {              
                return Ok(result);
            }
            return BadRequest(new ResponseVM
            {
                Message = "Wrong Email Or Password",
                Errors = new string[] {"Email or Password Do Not Match"}
            });
        }

        [HttpPost("UpdateProfile/{id}", Name = "Update Current User Profile")]
        [ProducesResponseType(typeof(ResponseVMWithEntity<AccountRequestVM>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseVM), StatusCodes.Status400BadRequest)]
        public IActionResult UpdateProfile(int id, [FromForm] AccountRequestVM accountRequest)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new ResponseVM
                    {
                        Message = "Email Is Required",
                        Errors = new string[] { "Email Is Required" }
                    });
                }
                var oldAccount = _accountService.GetAccountById(id);
                var newProfile = new NewProfile
                {
                    newUserName = accountRequest.UserName == null ? oldAccount.UserName : accountRequest.UserName,
                    Email = accountRequest.Email,
                    newAddress = accountRequest.Address == null ? oldAccount.Address : accountRequest.Address,
                    newCountry = accountRequest.Country == null ? oldAccount.Country : accountRequest.Country,
                    newPhone = accountRequest.Phone == null ? oldAccount.Phone : accountRequest.Phone,
                };
                var result = _accountService.UpdateAccount(id, newProfile);
                if (!result)
                {
                    return BadRequest(new ResponseVM
                    {
                        Message = "Cannot Update Account",
                        Errors = new string[] {"Error While Updating to Database"}
                    });
                }

                //upload identity and driver license images
                if (accountRequest.LicenseImage != null)
                {
                    //upload file to cloudinary
                    var licenseStream = accountRequest.LicenseImage.OpenReadStream();
                    var uploadLicenseParams = new ImageUploadParams
                    {
                        File = new FileDescription(accountRequest.LicenseImage.FileName, licenseStream),
                        Folder = "exe201/License"
                    };
                    var uploadLicenseResult = _cloudinary.Upload(uploadLicenseParams);


                    //add image to database
                    ImagesLicenseCard LicenseImage = new ImagesLicenseCard
                    {
                        ImagesLink = uploadLicenseResult.Url.ToString(),
                        ImagesType = "LICENSE",
                        AccountId = id,
                    };
                    var check = _accountService.AddLicenseImage(LicenseImage);
                    if (!check)
                    {
                        return BadRequest(new ResponseVM
                        {
                            Message = $"Cannot Upload Image {accountRequest.LicenseImage.FileName}",
                            Errors = new string[] { "Error Saving Image to Database" }
                        });
                    }
                }
                if (accountRequest.IdentityImage != null)
                {
                    //upload file to cloudinary
                    var identityStream = accountRequest.IdentityImage.OpenReadStream();
                    var uploadIdentityParams = new ImageUploadParams
                    {
                        File = new FileDescription(accountRequest.IdentityImage.FileName, identityStream),
                        Folder = "exe201/Identity"
                    };
                    var uploadIdentityResult = _cloudinary.Upload(uploadIdentityParams);


                    //add image to database
                    ImagesLicenseCard IdentityImage = new ImagesLicenseCard
                    {
                        ImagesLink = uploadIdentityResult.Url.ToString(),
                        ImagesType = "IDENTITY",
                        AccountId = id,
                    };
                    var check = _accountService.AddLicenseImage(IdentityImage);
                    if (!check)
                    {
                        return BadRequest(new ResponseVM
                        {
                            Message = $"Cannot Upload Image {accountRequest.IdentityImage.FileName}",
                            Errors = new string[] { "Error Saving Image to Database" }
                        });
                    }
                }


                return Ok(new ResponseVMWithEntity<AccountRequestVM>
                {
                    Message = "Update Successfully",
                    Entity = accountRequest,
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseVM
                {
                    Message = "Cannot Update Account",
                    Errors = new string[] {ex.Message }
                });
            }
        }

        [HttpGet("GetAccountProfile/{id}", Name = "Get Current User Profile")]
        [ProducesResponseType(typeof(ResponseVMWithEntity<AccountProfileDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseVM), StatusCodes.Status404NotFound)]
        public IActionResult ViewCurrentUserProfile(int id)
        {
            var account = _accountService.GetAccountProfileById(id);
            if (account == null)
            {
                return NotFound(new ResponseVM {
                    Message = "Cannot Find Profile",
                    Errors = new string[] { "There's No Profile With ID " + id }
                });
            }
            return Ok(new ResponseVMWithEntity<AccountProfileDTO>
            {
                Message = "Find Successfully",
                Entity = account,
            });
        }
    }
}