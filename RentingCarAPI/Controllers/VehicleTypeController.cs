using BusinessObjects.Models;
using Microsoft.AspNetCore.Mvc;
using RentingCarAPI.ViewModel;
using RentingCarServices.ServiceInterface;

namespace RentingCarAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VehicleTypeController : ControllerBase
    {
        private readonly ILogger<VehicleTypeController> _logger;
        private readonly IVehicleTypeService _vehicleTypeService;

        public VehicleTypeController(ILogger<VehicleTypeController> logger, IVehicleTypeService vehicleTypeService)
        {
            _logger = logger;
            _vehicleTypeService = vehicleTypeService;
        }
        [HttpGet("getall")]
        [ProducesResponseType(typeof(List<VehicleType>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseVM), StatusCodes.Status404NotFound)]
        public IActionResult GetAll()
        {
            List<VehicleType> typeList = _vehicleTypeService.GetVehicleTypes();
            if (!typeList.Any())
            {
                return NotFound(new ResponseVM { 
                    Message = "There's No Vehicle List",
                    Errors = new string[] {"Vehicle Type List is Empty"}
                });
            }
            return Ok(typeList);
        }
        [HttpPost("create")]
        [ProducesResponseType(typeof(ResponseVMWithEntity<VehicleType>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseVM), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResponseVMWithEntity<VehicleType>), StatusCodes.Status400BadRequest)]
        public IActionResult Create(string name)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(new ResponseVM
                {
                    Message = "Invalid Input",
                    Errors = new string[] {"Name is null", "Name must have at least 50 characters"}
                });
            }
            VehicleType newVehicleType = new VehicleType();
            newVehicleType.TypeName = name;
            bool check = _vehicleTypeService.Add(newVehicleType);
            if(!check)
            {
                return BadRequest(new ResponseVMWithEntity<VehicleType>
                {
                    Message = "Invalid Input",
                    Entity = newVehicleType,
                    Errors = new string[] {"Invalid Data to Database", "Cannot Save or Update to Database"}
                });
            }

            return Ok(new ResponseVMWithEntity<VehicleType>
            {
                Message = "Create Successfully",
                Entity = newVehicleType
            });
        }
        [HttpPut("update/{id}")]
        [ProducesResponseType(typeof(ResponseVMWithEntity<VehicleType>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseVM), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResponseVMWithEntity<VehicleType>), StatusCodes.Status400BadRequest)]
        public ActionResult<VehicleType> Update([FromRoute] long id,string name)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ResponseVM
                {
                    Message = "Invalid Data",
                    Errors = new string[] { "Name is null", "Name must have at least 50 characters" }
                });
            }
            VehicleType updateType = _vehicleTypeService.GetVehicleTypeById(id);
            if (updateType != null)
            {
                updateType.TypeName = name;
            }
            bool check = _vehicleTypeService.Update(updateType);
            if(!check)
            {
                return BadRequest(new ResponseVMWithEntity<VehicleType>
                {
                    Message = "Invalid Input",
                    Entity = updateType,
                    Errors = new string[] {"Invalid Data to Database", "Cannot Save or Update to Database" }
                });
            }
            return Ok(new ResponseVMWithEntity<VehicleType>
            {
                Message = "Update Successfully",
                Entity = updateType
            });
        }
        [HttpDelete("delete/{id}")]
        [ProducesResponseType(typeof(ResponseVMWithEntity<VehicleType>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseVM), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResponseVMWithEntity<VehicleType>), StatusCodes.Status404NotFound)]
        public IActionResult Delete([FromRoute] long id) {
            VehicleType delType = _vehicleTypeService.GetVehicleTypeById(id);
            if (delType == null)
            {
                return NotFound(new ResponseVM
                {
                    Message = "Cannot find type with id" +id,
                    Errors = new string[] {"There's no data in database with id" +id}
                });
            }
            bool check = _vehicleTypeService.Delete(delType);
            if (!check)
            {
                return BadRequest(new ResponseVMWithEntity<VehicleType>
                {
                    Message = "Invalid Data",
                    Entity = delType,
                    Errors = new string[] {"Cannot Save or Update to Database", "Invalid Data to Database"}
                });
            }
            return Ok(new ResponseVMWithEntity<VehicleType>
            {
                Message = "Delete Successfully",
                Entity = delType
            });
        }
    }
}
