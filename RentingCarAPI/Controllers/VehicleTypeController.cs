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
        [HttpGet("GetVehicleTypes", Name = "Get All Vehicle Type")]
        [ProducesResponseType(typeof(List<VehicleType>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseVM), StatusCodes.Status404NotFound)]
        public IActionResult GetAll()
        {
            try
            {
                List<VehicleType> typeList = _vehicleTypeService.GetVehicleTypes();
                if (!typeList.Any())
                {
                    return NotFound(new ResponseVM
                    {
                        Message = "Cannot Find Vehicle Type List",
                        Errors = new string[] { "No Data in Database" }
                    });
                }
                return Ok(typeList);
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseVM
                {
                    Message = "Cannot Find Vehicle Type List",
                    Errors = new string[] { "No Data in Database" }
                });
            }
        }
        [HttpPost("CreateVehicleType", Name = "Create A Vehicle Type")]
        [ProducesResponseType(typeof(ResponseVMWithEntity<VehicleType>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseVM), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResponseVMWithEntity<VehicleType>), StatusCodes.Status400BadRequest)]
        public IActionResult Create(string name)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new ResponseVM
                    {
                        Message = "Name Must Not Be Empty",
                        Errors = new string[] { "Name is null" }
                    });
                }
                VehicleType newVehicleType = new VehicleType();
                newVehicleType.TypeName = name;
                bool check = _vehicleTypeService.Add(newVehicleType);
                if (!check)
                {
                    return BadRequest(new ResponseVMWithEntity<VehicleType>
                    {
                        Message = "Cannot Create Vehicle Type",
                        Entity = newVehicleType,
                        Errors = new string[] { "Invalid Data to Database", "Cannot Save or Update to Database" }
                    });
                }

                return Ok(new ResponseVMWithEntity<VehicleType>
                {
                    Message = "Create Successfully",
                    Entity = newVehicleType
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseVM
                {
                    Message = "Cannot Create Vehicle Type",
                    Errors = new string[] { "Invalid Input", ex.Message }
                });
            }
        }
        [HttpPut("UpdateVehicleType/{id}", Name = "Update A Vehicle Type")]
        [ProducesResponseType(typeof(ResponseVMWithEntity<VehicleType>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseVM), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResponseVMWithEntity<VehicleType>), StatusCodes.Status400BadRequest)]
        public ActionResult<VehicleType> Update([FromRoute] long id, string name)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new ResponseVM
                    {
                        Message = "Name Must Not Be Empty",
                        Errors = new string[] { "Name is null", "Name must have at least 50 characters" }
                    });
                }
                VehicleType updateType = _vehicleTypeService.GetVehicleTypeById(id);
                if (updateType != null)
                {
                    updateType.TypeName = name;
                }
                bool check = _vehicleTypeService.Update(updateType);
                if (!check)
                {
                    return BadRequest(new ResponseVMWithEntity<VehicleType>
                    {
                        Message = "Cannot Update Vehicle Type",
                        Entity = updateType,
                        Errors = new string[] { "Invalid Data to Database", "Cannot Save or Update to Database" }
                    });
                }
                return Ok(new ResponseVMWithEntity<VehicleType>
                {
                    Message = "Update Successfully",
                    Entity = updateType
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseVM
                {
                    Message = "Cannot Update Vehicle Type",
                    Errors = new string[] { "Invalid Input", ex.Message }
                });
            }
        }
        [HttpDelete("DeleteVehicleTypes/{id}", Name = "Delete A Vehicle Type")]
        [ProducesResponseType(typeof(ResponseVMWithEntity<VehicleType>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseVM), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResponseVMWithEntity<VehicleType>), StatusCodes.Status404NotFound)]
        public IActionResult Delete([FromRoute] long id)
        {
            try
            {
                VehicleType delType = _vehicleTypeService.GetVehicleTypeById(id);
                if (delType == null)
                {
                    return NotFound(new ResponseVM
                    {
                        Message = "Cannot find type with id" + id,
                        Errors = new string[] { "There's no data in database with id" + id }
                    });
                }
                bool check = _vehicleTypeService.Delete(delType);
                if (!check)
                {
                    return BadRequest(new ResponseVMWithEntity<VehicleType>
                    {
                        Message = "Cannot Remove Vehicle Type",
                        Entity = delType,
                        Errors = new string[] { "Cannot Save or Update to Database", "Invalid Data to Database" }
                    });
                }
                return Ok(new ResponseVMWithEntity<VehicleType>
                {
                    Message = "Remove Successfully",
                    Entity = delType
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseVM
                {
                    Message = "Cannot Remove Vehicle Type",
                    Errors = new string[] { "Invalid Input", ex.Message }
                });
            }
        }
    }
}
