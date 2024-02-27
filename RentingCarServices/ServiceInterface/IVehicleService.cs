using BusinessObjects.Models;
using RentingCarDAO.DTO;

namespace RentingCarServices.ServiceInterface
{
    public interface IVehicleService
    {
        public List<VehicleDTO> GetAllVehicles();
        public List<VehicleImage> GetVehicleImages();
        public List<Vehicle> GetVehicles();
        public Vehicle? GetVehicleById(long id);
        public bool AddVehicle(Vehicle vehicle);
        bool UpdateVehicle(Vehicle vehicle);
        bool DeleteVehicle(Vehicle vehicle);
        bool AddVehicleImage(VehicleImage image);
        bool UpdateVehicleImage(VehicleImage image);
        bool DeleteVehicleImage(VehicleImage image);
    }
}
