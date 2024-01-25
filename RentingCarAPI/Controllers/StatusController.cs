using Microsoft.AspNetCore.Mvc;
using RentingCarServices.Service;
using RentingCarServices.ServiceInterface;

namespace RentingCarAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StatusController : ControllerBase
    {
        private readonly ILogger<StatusController> _logger;
        private readonly IStatusService _statusService;

        public StatusController(ILogger<StatusController> logger, IStatusService statusService)
        {
            _logger = logger;
            _statusService = statusService;
        }
        [HttpGet("getStatuses")]
        public IActionResult GetStatuses()
        {
            var list = _statusService.GetStatuses();
            if(!list.Any())
            {
                return NotFound();
            }
            return Ok(list);
        }
        [HttpPut("updateStatus/{id}")]
        public IActionResult Update([FromRoute] long id, string name)
        {
            var status = _statusService.GetStatusById(id);
            if (status != null)
            {
                status.StatusName = name;
            }
            bool check = _statusService.Update(status);
            if (!check)
            {
                return BadRequest();
            }
            return Ok("Update Successfully");
        }
    }
}
