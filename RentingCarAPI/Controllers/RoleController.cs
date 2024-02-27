using BusinessObjects.Models;
using Microsoft.AspNetCore.Mvc;
using RentingCarAPI.ViewModel;
using RentingCarServices.ServiceInterface;

namespace RentingCarAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RoleController : ControllerBase
    {
        private readonly ILogger<RoleController> _logger;
        private readonly IRoleService _roleService;

        public RoleController(ILogger<RoleController> logger, IRoleService roleService)
        {
            _logger = logger;
            _roleService = roleService;
        }
        [HttpGet("GetRoles", Name = "Get All Roles")]
        [ProducesResponseType(typeof(List<Role>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseVM), StatusCodes.Status404NotFound)]
        public IActionResult GetRoles()
        {
            try
            {
                var list = _roleService.GetRoles();
                if (!list.Any())
                {
                    return NotFound(new ResponseVM
                    {
                        Message = "Cannot Find Role List",
                        Errors = new string[] { "No Data in Database" }
                    });
                }
                return Ok(list);
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseVM
                {
                    Message = "Cannot Find Role List",
                    Errors = new string[] { "Error While Getting Data", ex.Message }
                });
            }
        }
        [HttpPost("CreateRole", Name = "Create A New Role")]
        [ProducesResponseType(typeof(ResponseVMWithEntity<Role>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseVM), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResponseVMWithEntity<Role>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResponseVM), StatusCodes.Status404NotFound)]
        public IActionResult CreateRole(string name)
        {
            try
            {
                if (name == null)
                {
                    return BadRequest(new ResponseVM
                    {
                        Message = "Role Name Must Be Inputted",
                        Errors = new string[] { "Role Name Cannot Be Null" }
                    });
                }
                Role newRole = new Role { RoleName = name };
                var check = _roleService.Add(newRole);
                if (!check)
                {
                    return BadRequest(new ResponseVMWithEntity<Role>
                    {
                        Message = "Cannot Create Role",
                        Errors = new string[] { "Invalid Input" },
                        Entity = newRole
                    });
                }
                return Ok(new ResponseVMWithEntity<Role>
                {
                    Message = "Create Successfully",
                    Entity = newRole
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseVM
                {
                    Message = "Cannot Create Role",
                    Errors = new string[] { "Invalid Input", ex.Message }
                });
            }
        }
        [HttpPut("UpdateRole/{id}", Name = "Update A Role")]
        [ProducesResponseType(typeof(ResponseVMWithEntity<Role>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseVM), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResponseVMWithEntity<Role>), StatusCodes.Status400BadRequest)]
        public IActionResult Update([FromRoute] long id, string name)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new ResponseVM
                    {
                        Message = "Invalid Input",
                        Errors = new string[] { "Name is null", "Name doesn't have 50 characters" }
                    });
                }
                var role = _roleService.GetRoleById(id);
                if (role != null)
                {
                    role.RoleName = name;
                }
                else
                {
                    return BadRequest(new ResponseVM
                    {
                        Message = "Cannot Update Role",
                        Errors = new string[] { "No Data Found With ID " + id }
                    });
                }
                bool check = _roleService.Update(role);
                if (!check)
                {
                    return BadRequest(new ResponseVMWithEntity<Role>
                    {
                        Message = "Cannot Update Role",
                        Entity = role,
                        Errors = new string[] { "Invalid Data to Database", "Cannot Update or Save to Database" }
                    });
                }
                return Ok(new ResponseVMWithEntity<Role>
                {
                    Message = "Update Successfully",
                    Entity = role
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseVM
                {
                    Message = "Cannot Update Role",
                    Errors = new string[] { "Invalid Input", ex.Message }
                });
            }
        }

        /*[HttpGet("CheckRole")]
        public IActionResult Example()
        {
            var roles = User.FindFirst(ClaimTypes.Role)?.Value;
            _logger.LogInformation($"User roles: {roles}");

            return Ok("Example");
        }*/
    }
}
