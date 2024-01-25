using BusinessObjects.Models;
using Microsoft.AspNetCore.Mvc;
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
        [HttpGet("getAllVehicleTypes")]
        public IActionResult GetAll()
        {
            List<VehicleType> typeList = _vehicleTypeService.GetVehicleTypes();
            if (!typeList.Any())
            {
                return NotFound("There is no vehicle type");
            }
            return Ok(typeList);
        }
        [HttpPost("createType")]
        public IActionResult Create(string name)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }
            VehicleType newVehicleType = new VehicleType();
            newVehicleType.TypeName = name;
            bool check = _vehicleTypeService.Add(newVehicleType);
            if(!check)
            {
                return BadRequest();
            }

            return Ok("Create Successfully\n" +newVehicleType);
        }
        [HttpPut("updateType/{id}")]
        public IActionResult Update([FromRoute] long id,string name)
        {
            VehicleType updateType = _vehicleTypeService.GetVehicleTypeById(id);
            if (updateType != null)
            {
                updateType.TypeName = name;
            }
            bool check = _vehicleTypeService.Update(updateType);
            if(!check)
            {
                return BadRequest();
            }
            return Ok("Update Successfully");
        }
        [HttpDelete("deleteType/{id}")]
        public IActionResult Delete([FromRoute] long id) {
            VehicleType delType = _vehicleTypeService.GetVehicleTypeById(id);
            bool check = _vehicleTypeService.Delete(delType);
            if (!check)
            {
                return BadRequest();
            }
            return Ok("Delete Successfully Type With ID: " +id);
        }
    }
}
