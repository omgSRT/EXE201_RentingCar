using BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
