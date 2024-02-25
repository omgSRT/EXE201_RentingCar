using BusinessObjects.Models;
using CloudinaryDotNet.Actions;
using CloudinaryDotNet;
using Microsoft.AspNetCore.Mvc;
using RentingCarAPI.ViewModel;
using RentingCarDAO.DTO;
using RentingCarServices.Service;
using RentingCarServices.ServiceInterface;
using System;
using System.ComponentModel.DataAnnotations;
using System.Net.WebSockets;
using RentingCarDAO;

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
        [ProducesResponseType(typeof(ResponseVM), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(List<Vehicle>), StatusCodes.Status200OK)]
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
        [ProducesResponseType(typeof(ResponseVM), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Vehicle), StatusCodes.Status200OK)]
        public IActionResult GetVehicleById(long id)
        {
            Vehicle? vehicle = _vehicleService.GetVehicleById(id);
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
        public IActionResult AddVehicle([FromForm] ViewModel.VehicleAddRequestVM vehicleDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new ResponseVM
                    {
                        Message = "Invalid Data",
                        Errors = new string[] { "Invalid Data To Model" }
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
                    Message = "Cannot Create Vehicle",
                    Errors = new string[] {"Error While Inserting Data"}
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseVM
                {
                    Message = "Cannot Create Vehicle",
                    Errors = new string[] { "Invalid Input", ex.Message }
                });
            }

        }

        [HttpPut("UpdateVehicle/{id}", Name = "Update Existed Vehicle")]
        [ProducesResponseType(typeof(ResponseVM), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResponseVMWithEntity<Vehicle>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResponseVM), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ResponseVMWithEntity<Vehicle>), StatusCodes.Status200OK)]
        public IActionResult UpdateVehicle([FromRoute] long id, 
            [FromForm] ViewModel.VehicleUpdateRequestVM requestVehicle)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new ResponseVM
                    {
                        Message = "Invalid Data",
                        Errors = new string[] { "Invalid Data To Model" }
                    });
                }
                var updateVehicle = _vehicleService.GetVehicleById(id);
                if (updateVehicle == null)
                {
                    return NotFound(new ResponseVM
                    {
                        Message = "Cannot Found Vehicle With ID " +id,
                        Errors = new string[] {"There's No Vehicle Data With ID " +id}
                    });
                }

                //update existed vehicle
                updateVehicle.VehicleName = requestVehicle.VehicleName ?? "Random Car Name";
                updateVehicle.Passengers = requestVehicle.Passengers <= 0 ? 1 : requestVehicle.Passengers;
                updateVehicle.Suitcase = requestVehicle.Suitcase ?? "AutoPack";
                updateVehicle.Doors = requestVehicle.Doors < 0 ? 0 : requestVehicle.Doors;
                updateVehicle.Engine = requestVehicle.Engine ?? "Internal Combustion";
                updateVehicle.Fueltype = requestVehicle.Fueltype ?? "Gasoline";
                updateVehicle.Options = requestVehicle.Options ?? "Options";
                updateVehicle.Amount = requestVehicle.Amount <= 0 ? 1 : requestVehicle.Amount;
                updateVehicle.Deposit = requestVehicle.Deposit <= 0 ? 10 : requestVehicle.Deposit;
                updateVehicle.Price = requestVehicle.Price <= 0 ? 10 : requestVehicle.Price;
                updateVehicle.LicensePlate = requestVehicle.LicensePlate ?? "ABC123";
                updateVehicle.ModelType = requestVehicle.ModelType ?? "AUTO";
                updateVehicle.Location = requestVehicle.Location ?? "TPHCM";
                updateVehicle.VehicleTypeId = requestVehicle.VehicleTypeId <= 0 ? 1 : requestVehicle.VehicleTypeId;

                var check = _vehicleService.UpdateVehicle(updateVehicle);
                if (!check)
                {
                    return BadRequest(new ResponseVMWithEntity<Vehicle>
                    {
                        Message = "Cannot Update Vehicle",
                        Errors = new string[] {"Invalid Data To Database"},
                        Entity = updateVehicle
                    });
                }

                return Ok(new ResponseVMWithEntity<Vehicle>
                {
                    Message = "Update Successfully",
                    Entity = updateVehicle
                });
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

        [HttpPut("DeleteVehicle/{id}", Name = "Delete A Vehicle")]
        [ProducesResponseType(typeof(ResponseVM), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResponseVMWithEntity<Vehicle>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResponseVM), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ResponseVMWithEntity<Vehicle>), StatusCodes.Status200OK)]
        public IActionResult DeleteVehicle([FromRoute] long id)
        {
            try
            {
                var deleteVehicle = _vehicleService.GetVehicleById(id);
                if (deleteVehicle == null)
                {
                    return NotFound(new ResponseVM
                    {
                        Message = "Cannot Found Vehicle To Remove",
                        Errors = new string[] { "There's No Vehicle Data With ID " + id }
                    });
                }

                deleteVehicle.StatusId = 2;
                var check = _vehicleService.UpdateVehicle(deleteVehicle);
                if (!check)
                {
                    return BadRequest(new ResponseVMWithEntity<Vehicle>
                    {
                        Message = "Cannot Remove Vehicle",
                        Errors = new string[] { "Invalid Data To Database" },
                        Entity = deleteVehicle
                    });
                }

                return Ok(new ResponseVMWithEntity<Vehicle>
                {
                    Message = "Remove Successfully",
                    Entity = deleteVehicle
                });
            }
            catch(Exception ex)
            {
                return BadRequest(new ResponseVM
                {
                    Message = "Cannot Remove Account",
                    Errors = new string[] { "Invalid Input", ex.Message }
                });
            }
        }

        [HttpPost("VehicleImage/Add/{vehicleId}", Name = "Add Vehicle Images to Existing Vehicle")]
        [ProducesResponseType(typeof(ResponseVM), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(List<IFormFile>), StatusCodes.Status200OK)]
        public IActionResult AddMoreImages([FromRoute] long vehicleId, List<IFormFile> imageList)
        {
            try
            {
                var foundVehicle = _vehicleService.GetVehicleById(vehicleId);
                if (foundVehicle == null)
                {
                    return NotFound(new ResponseVM
                    {
                        Message = "Cannot Found Vehicle",
                        Errors = new string[] { "There's No Vehicle Data With ID " + vehicleId }
                    });
                }

                var id = foundVehicle.VehicleId;
                if (imageList != null)
                {
                    foreach (var file in imageList)
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
                                VehicleId = id,
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

                return Ok(imageList);
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseVM
                {
                    Message = "Cannot Update Account",
                    Errors = new string[] { "Invalid Input", ex.Message }
                });
            }
        }

        [HttpPost("VehicleImage/Replace/{vehicleId}", Name = "Add Vehicle Images to Existing Vehicle")]
        [ProducesResponseType(typeof(ResponseVM), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(List<IFormFile>), StatusCodes.Status200OK)]
        public IActionResult ReplaceImages([FromRoute] long vehicleId, List<IFormFile> imageList)
        {
            try
            {
                var foundVehicle = _vehicleService.GetVehicleById(vehicleId);
                if (foundVehicle == null)
                {
                    return NotFound(new ResponseVM
                    {
                        Message = "Cannot Found Vehicle",
                        Errors = new string[] { "There's No Vehicle Data With ID " + vehicleId }
                    });
                }

                var existImages = _vehicleService.GetVehicleImages().Where(x => x.VehicleId == vehicleId);
                var failedDeletions = new List<VehicleImage>();
                if (existImages.Any())
                {
                    var backupImages = existImages;
                    foreach (var image in existImages)
                    {
                        var checkDeleteImage = _vehicleService.DeleteVehicleImage(image);
                        if (!checkDeleteImage)
                        {
                            failedDeletions.Add(image);
                        }
                    }

                    if(failedDeletions.Any())
                    {
                        foreach(var image in failedDeletions)
                        {
                            var check = _vehicleService.AddVehicleImage(image);
                            if (!check)
                            {
                                return BadRequest(new ResponseVM
                                {
                                    Message = "Cannot Upload Images",
                                    Errors = new string[] { "Error While Inserting Data" }
                                });
                            }
                        }
                        return BadRequest(new ResponseVM
                        {
                            Message = "Cannot Delete Some Images. Images Has Been Restored",
                            Errors = failedDeletions.Select(x => $"Error While Deleting Image with ID: {x.ImagesId}").ToArray()
                        });
                    }
                }

                var id = foundVehicle.VehicleId;
                if (imageList != null)
                {
                    foreach (var file in imageList)
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
                                VehicleId = id,
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

                return Ok(imageList);
            }
            catch (Exception ex)
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
