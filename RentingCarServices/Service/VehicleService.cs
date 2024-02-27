using BusinessObjects.Models;
using Microsoft.Extensions.Configuration;
using RentingCarDAO.DTO;
using RentingCarRepositories.RepositoryInterface;
using RentingCarServices.ServiceInterface;

namespace RentingCarServices.Service
{
    public class VehicleService : IVehicleService
    {
        private readonly IConfiguration _configuration;
        public readonly IVehicleRepository _vehicleRepository;

        public VehicleService(IVehicleRepository vehicleRepository, IConfiguration configuration)
        {
            this._configuration = configuration;
            _vehicleRepository = vehicleRepository;
        }

        public List<VehicleDTO> GetAllVehicles()
        {
            try
            {
                IEnumerable<Vehicle> vehicleList = _vehicleRepository.GetVehicles();
                if (vehicleList == null)
                {
                    return new List<VehicleDTO>();
                }
                else
                {
                    List<VehicleDTO> vehicleDTOs = vehicleList.Select(v => new VehicleDTO
                    {
                        VehicleId = v.VehicleId,
                        VehicleName = v.VehicleName,
                        Price = v.Price,
                        Fueltype = v.Fueltype,
                        Passengers = v.Passengers,
                        ModelType = v.ModelType,
                        TypeName = v.VehicleType?.TypeName,
                        VehicleImage = v.VehicleImages?.FirstOrDefault()?.ToString(),
                    }).Where(dto => dto != null).ToList();
                    return vehicleDTOs;
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public Vehicle? GetVehicleById(long id)
        {
            return _vehicleRepository.GetVehicleById(id);

        }

        public bool AddVehicle(Vehicle vehicle)
        {
            return _vehicleRepository.AddVehicle(vehicle);
        }

        public bool AddVehicleImage(VehicleImage image)
        {
            return _vehicleRepository.AddVehicleImage(image);
        }

        public bool UpdateVehicle(Vehicle vehicle)
        {
            return _vehicleRepository.UpdateVehicle(vehicle);
        }

        public bool RemoveVehicle(Vehicle vehicle)
        {
            return _vehicleRepository.RemoveVehicle(vehicle);
        }

        public bool UpdateVehicleImage(VehicleImage image)
        {
            return _vehicleRepository.UpdateVehicleImage(image);
        }

        public bool RemoveVehicleImage(VehicleImage image)
        {
            return _vehicleRepository.RemoveVehicleImage(image);
        }

        public List<VehicleImage> GetVehicleImages()
        {
            return _vehicleRepository.GetVehicleImages().ToList();
        }

        public List<Vehicle> GetVehicles()
        {
            return _vehicleRepository.GetVehicles().ToList();
        }

        public bool DeleteVehicle(Vehicle vehicle)
        {
            throw new NotImplementedException();
        }

        public bool DeleteVehicleImage(VehicleImage image)
        {
            throw new NotImplementedException();
        }
    }
}
