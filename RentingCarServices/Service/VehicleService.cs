using BusinessObjects.Models;
using Microsoft.Extensions.Configuration;
using RentingCarDAO.DTO;
using RentingCarRepositories.RepositoryInterface;
using RentingCarServices.ServiceInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                if(vehicleList == null)
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
                        ImagesLink = v.VehicleImages?.FirstOrDefault()?.ToString(),
                    }).Where(dto => dto != null).ToList();
                    return vehicleDTOs;
                }
                
            }catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            
        }

        public Vehicle? GetVehicleById(long id)
        {
            return _vehicleRepository.GetVehicleById(id);

        }
    }
}
