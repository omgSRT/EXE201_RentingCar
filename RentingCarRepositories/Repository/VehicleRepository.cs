using BusinessObjects.Models;
using RentingCarDAO;
using RentingCarRepositories.RepositoryInterface;

namespace RentingCarRepositories.Repository
{
    public class VehicleRepository : IVehicleRepository
    {
        private readonly VehicleDAO _vehicleDAO;
        public VehicleRepository()
        {
            _vehicleDAO = new VehicleDAO();
        }
        public bool AddVehicle(Vehicle vehicle)
        {
            return _vehicleDAO.AddVehicle(vehicle);
        }

        public bool AddVehicleImage(VehicleImage image)
        {
            return _vehicleDAO.AddVehicleImage(image);
        }

        public Vehicle? GetVehicleById(long id)
        {
            return _vehicleDAO.GetVehicleById(id);
        }

        public VehicleImage? GetVehicleImageById(long id)
        {
            return _vehicleDAO.GetVehicleImageById(id);
        }

        public IEnumerable<VehicleImage> GetVehicleImages()
        {
            return _vehicleDAO.GetVehicleImages();
        }

        public IEnumerable<Vehicle> GetVehicles()
        {
            return _vehicleDAO.GetVehicles();
        }

        public bool RemoveVehicle(Vehicle vehicle)
        {
            return _vehicleDAO.RemoveVehicle(vehicle);
        }

        public bool RemoveVehicleImage(VehicleImage image)
        {
            return _vehicleDAO.RemoveVehicleImage(image);
        }

        public bool UpdateVehicle(Vehicle vehicle)
        {
            return _vehicleDAO.UpdateVehicle(vehicle);
        }

        public bool UpdateVehicleImage(VehicleImage image)
        {
            return _vehicleDAO.UpdateVehicleImage(image);
        }
    }
}
