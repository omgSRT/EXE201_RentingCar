using BusinessObjects.Models;
using Microsoft.AspNetCore.Mvc;
using RentingCarAPI.ViewModel;
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
        [HttpGet("GetStatus", Name = "Get All Status")]
        [ProducesResponseType(typeof(List<Status>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseVM), StatusCodes.Status404NotFound)]
        public ActionResult<List<Status>> GetStatuses()
        {
            try
            {
                var list = _statusService.GetStatuses();
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
                    Message = "Cannot Find Status List",
                    Errors = new string[] { "Error While Getting Data", ex.Message }
                });
            }
        }
        [HttpPut("UpdateStatus/{id}", Name = "Update A Status")]
        [ProducesResponseType(typeof(ResponseVMWithEntity<Status>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseVM), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResponseVMWithEntity<Status>), StatusCodes.Status400BadRequest)]
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
                var status = _statusService.GetStatusById(id);
                if (status != null)
                {
                    status.StatusName = name;
                }
                else
                {
                    return BadRequest(new ResponseVM
                    {
                        Message = "Cannot Update Status",
                        Errors = new string[] { "No Status Data With ID " +id }
                    });
                }
                bool check = _statusService.Update(status);
                if (!check)
                {
                    return BadRequest(new ResponseVMWithEntity<Status>
                    {
                        Message = "Cannot Update Status",
                        Entity = status,
                        Errors = new string[] { "Invalid Data to Database", "Cannot Update or Save to Database" }
                    });
                }
                return Ok(new ResponseVMWithEntity<Status>
                {
                    Message = "Update Successfully",
                    Entity = status
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseVM
                {
                    Message = "Cannot Update Status",
                    Errors = new string[] { "Invalid Input", ex.Message }
                });
            }
        }
    }
}
