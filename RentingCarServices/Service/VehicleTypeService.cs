using BusinessObjects.Models;
using RentingCarRepositories.RepositoryInterface;
using RentingCarServices.ServiceInterface;

namespace RentingCarServices.Service
{
    public class VehicleTypeService : IVehicleTypeService
    {
        public readonly IVehicleTypeRepository _vehicleTypeRepository;

        public VehicleTypeService(IVehicleTypeRepository vehicleTypeRepository)
        {
            _vehicleTypeRepository = vehicleTypeRepository;
        }

        public bool Add(VehicleType vehicleType)
        {
            return _vehicleTypeRepository.Add(vehicleType);
        }

        public bool Delete(VehicleType vehicleType)
        {
            return _vehicleTypeRepository.Delete(vehicleType);
        }

        public VehicleType GetVehicleTypeById(long id)
        {
            return _vehicleTypeRepository.GetVehicleTypeById(id);
        }

        public VehicleType GetVehicleTypeByName(string searchType)
        {
            return _vehicleTypeRepository.GetVehicleTypeByName(searchType);
        }

        public List<VehicleType> GetVehicleTypes()
        {
            return _vehicleTypeRepository.GetVehicleTypes();
        }

        public bool Update(VehicleType vehicleType)
        {
            return _vehicleTypeRepository.Update(vehicleType);
        }
    }
}
