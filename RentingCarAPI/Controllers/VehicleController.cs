using Microsoft.AspNetCore.Mvc;
using RentingCarAPI.ViewModel;
using RentingCarDAO.DTO;
using RentingCarServices.Service;
using RentingCarServices.ServiceInterface;

namespace RentingCarAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VehicleController : Controller
    {
        private readonly ILogger<VehicleController> _logger;
        private readonly IVehicleService _vehicleService;

        public VehicleController(ILogger<VehicleController> logger, IVehicleService vehicleService)
        {
            _logger = logger;
            _vehicleService = vehicleService;
        }

        [HttpGet("SearchAllVehicle", Name = "Search All Vehicle")]
        //[Authorize(Roles = "USER")]
        public IActionResult GetAllAccount()
        {

            var vehicleList = _vehicleService.GetAllVehicles();
            if (!vehicleList.Any())
            {
                return NotFound(new ResponseVM
                {
                    Message = "There's No Data",
                    Errors = new string[] { "There's No Data" }
                });
            }
            return Ok(vehicleList);
        }
    }
}
