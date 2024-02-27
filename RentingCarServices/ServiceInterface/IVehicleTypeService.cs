using BusinessObjects.Models;

namespace RentingCarServices.ServiceInterface
{
    public interface IVehicleTypeService
    {
        List<VehicleType> GetVehicleTypes();
        VehicleType GetVehicleTypeByName(string searchType);
        VehicleType GetVehicleTypeById(long id);
        bool Add(VehicleType vehicleType);
        bool Update(VehicleType vehicleType);
        bool Delete(VehicleType vehicleType);
    }
}
