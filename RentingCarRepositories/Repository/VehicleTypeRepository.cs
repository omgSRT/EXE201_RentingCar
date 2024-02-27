using BusinessObjects.Models;
using RentingCarDAO;
using RentingCarRepositories.RepositoryInterface;

namespace RentingCarRepositories.Repository
{
    public class VehicleTypeRepository : IVehicleTypeRepository
    {
        private VehicleTypeDAO _vehicleTypeDAO;
        public VehicleTypeRepository()
        {
            _vehicleTypeDAO = new VehicleTypeDAO();
        }
        public bool Add(VehicleType vehicleType)
        {
            return _vehicleTypeDAO.Add(vehicleType);
        }

        public bool Delete(VehicleType vehicleType)
        {
            return _vehicleTypeDAO.Remove(vehicleType);
        }

        public VehicleType GetVehicleTypeById(long id)
        {
            return _vehicleTypeDAO.GetVehicleTypeById(id);
        }

        public VehicleType GetVehicleTypeByName(string searchType)
        {
            return _vehicleTypeDAO.GetVehicleTypeByName(searchType);
        }

        public List<VehicleType> GetVehicleTypes()
        {
            return _vehicleTypeDAO.GetVehicleTypes();
        }

        public bool Update(VehicleType vehicleType)
        {
            return _vehicleTypeDAO.Update(vehicleType);
        }
    }
}
