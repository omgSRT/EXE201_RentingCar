using BusinessObjects.Models;

namespace RentingCarRepositories.RepositoryInterface
{
    public interface IVehicleRepository
    {
        IEnumerable<Vehicle> GetVehicles();
        Vehicle? GetVehicleById(long id);
        bool AddVehicle(Vehicle vehicle);
        bool UpdateVehicle(Vehicle vehicle);
        bool RemoveVehicle(Vehicle vehicle);
        IEnumerable<VehicleImage> GetVehicleImages();
        VehicleImage? GetVehicleImageById(long id);
        bool AddVehicleImage(VehicleImage image);
        bool UpdateVehicleImage(VehicleImage image);
        bool RemoveVehicleImage(VehicleImage image);

    }
}
