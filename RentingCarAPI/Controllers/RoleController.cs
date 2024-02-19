using BusinessObjects.Models;
using Microsoft.AspNetCore.Mvc;
using RentingCarServices.ServiceInterface;
using RentingCarAPI.ViewModel;
using System.Security.Claims;

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
        public IActionResult GetStatuses()
        {
            var list = _roleService.GetRoles();
            if(!list.Any())
            {
                return NotFound(new ResponseVM
                {
                    Message = "There's no data",
                    Errors = new string[] {"There's no data in database"}
                });
            }
            return Ok(list);
        }
        [HttpPut("UpdateRole/{id}", Name = "Update A Role")]
        [ProducesResponseType(typeof(ResponseVMWithEntity<Role>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseVM), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResponseVMWithEntity<Role>), StatusCodes.Status400BadRequest)]
        public IActionResult Update([FromRoute] long id, string name)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(new ResponseVM
                {
                    Message = "Invalid Input",
                    Errors = new string[] {"Name is null", "Name doesn't have 50 characters"}
                });
            }
            var role = _roleService.GetRoleById(id);
            if(role != null)
            {
                role.RoleName = name;
            }
            bool check = _roleService.Update(role);
            if (!check)
            {
                return BadRequest(new ResponseVMWithEntity<Role>
                {
                    Message = "Server Error. Cannot Update",
                    Entity = role,
                    Errors = new string[] {"Invalid Data to Database", "Cannot Update or Save to Database" }
                });
            }
            return Ok(new ResponseVMWithEntity<Role>
            {
                Message = "Update Successfully",
                Entity = role
            });
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
