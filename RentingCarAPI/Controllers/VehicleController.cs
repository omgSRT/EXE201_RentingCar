using BusinessObjects.Models;
using CloudinaryDotNet.Actions;
using CloudinaryDotNet;
using Microsoft.AspNetCore.Mvc;
using RentingCarAPI.ViewModel;
using RentingCarDAO.DTO;
using RentingCarServices.Service;
using RentingCarServices.ServiceInterface;
using System;

namespace RentingCarAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VehicleController : Controller
    {
        private readonly ILogger<VehicleController> _logger;
        private readonly IVehicleService _vehicleService;
        private readonly Cloudinary _cloudinary;

        public VehicleController(ILogger<VehicleController> logger, IVehicleService vehicleService, Cloudinary cloudinary)
        {
            _logger = logger;
            _vehicleService = vehicleService;
            _cloudinary = cloudinary;
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

        [HttpGet("GetVehicleById/{id}", Name = "Get Vehicle By Id")]
        public IActionResult GetVehicleById(long id)
        {
            Vehicle vehicle = _vehicleService.GetVehicleById(id);
            if (vehicle == null)
            {
                return NotFound(new ResponseVM
                {
                    Message = "Not Found This Vehicle",
                    Errors = new string[] { "Find Fail" }
                });
            }
            return Ok(vehicle);
        }

        [HttpPost("CreateNewVehicle", Name = "Add New Vehicle")]
        [ProducesResponseType(typeof(ResponseVM), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResponseVMWithEntity<Vehicle>), StatusCodes.Status200OK)]
        public IActionResult AddVehicle([FromForm] ViewModel.VehicleDTO vehicleDTO)
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
                var newVehicle = new Vehicle
                {
                    Amount = vehicleDTO.Amount <= 0 ? 1 : vehicleDTO.Amount,
                    VehicleName = vehicleDTO.VehicleName,
                    Passengers = vehicleDTO.Passengers,
                    Suitcase = vehicleDTO.Suitcase,
                    Doors = vehicleDTO.Doors,
                    Engine = vehicleDTO.Engine,
                    Fueltype = vehicleDTO.Fueltype,
                    Options = vehicleDTO.Options,
                    Deposit = vehicleDTO.Deposit,
                    Price = vehicleDTO.Price,
                    ModelType = vehicleDTO.ModelType,
                    Location = vehicleDTO.Location,
                    LicensePlate = vehicleDTO.LicensePlate,
                    StatusId = 1,
                    VehicleTypeId = vehicleDTO.VehicleTypeId,
                };
                bool checkSuccess = _vehicleService.AddVehicle(newVehicle);
                
                long insertedImagesId = newVehicle.VehicleId;

                //upload identity and driver license images
                if (vehicleDTO.VehicleImages != null)
                {
                    foreach (var file in vehicleDTO.VehicleImages)
                    {
                        if (file != null && file.Length != 0)
                        {
                            var stream = file.OpenReadStream();
                            var uploadParams = new ImageUploadParams
                            {
                                File = new FileDescription(file.FileName, stream),
                                Folder = "exe201/Vehicle"
                            };
                            var uploadResult = _cloudinary.Upload(uploadParams);
                            VehicleImage uploadImage = new VehicleImage
                            {
                                ImagesLink = uploadResult.Url.ToString(),
                                VehicleId = insertedImagesId,
                            };
                            var check = _vehicleService.AddVehicleImage(uploadImage);
                            if (!check)
                            {
                                return BadRequest(new ResponseVM
                                {
                                    Message = "Cannot Upload Images",
                                    Errors = new string[] { "Error While Inserting Data" }
                                });
                            }
                        }
                    }
                }
                if (checkSuccess)
                {
                    return Ok(new ResponseVMWithEntity<Vehicle>
                    {
                        Message = "Create Successfully",
                        Entity = newVehicle,
                    });
                }
                return BadRequest(new ResponseVM
                {
                    Message = "Cannot Add Vehicle With ID " +newVehicle.VehicleId,
                    Errors = new string[] {"Error While Inserting Data"}
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseVM
                {
                    Message = "Cannot Create Account",
                    Errors = new string[] { "Invalid Input", ex.Message }
                });
            }

        }
        [HttpPost("CreateNewVehicle", Name = "Add New Vehicle")]
        [ProducesResponseType(typeof(ResponseVM), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResponseVMWithEntity<Vehicle>), StatusCodes.Status200OK)]
        public IActionResult UpdateVehicle([FromForm] ViewModel.VehicleDTO vehicleDTO)
        {
            try
            {
                return Ok();
            }
            catch(Exception ex)
            {
                return BadRequest(new ResponseVM
                {
                    Message = "Cannot Update Account",
                    Errors = new string[] { "Invalid Input", ex.Message }
                });
            }
        }
    }
}
