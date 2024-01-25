using Microsoft.AspNetCore.Mvc;
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
        [HttpGet("getRoles")]
        public IActionResult GetStatuses()
        {
            var list = _roleService.GetRoles();
            if(!list.Any())
            {
                return NotFound();
            }
            return Ok(list);
        }
        [HttpPut("updateRole/{id}")]
        public IActionResult Update([FromRoute] long id, string name)
        {
            var role = _roleService.GetRoleById(id);
            if(role != null)
            {
                role.RoleName = name;
            }
            bool check = _roleService.Update(role);
            if (!check)
            {
                return BadRequest();
            }
            return Ok("Update Successfully");
        }
    }
}
