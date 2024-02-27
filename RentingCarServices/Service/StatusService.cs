using BusinessObjects.Models;
using RentingCarRepositories.RepositoryInterface;
using RentingCarServices.ServiceInterface;

namespace RentingCarServices.Service
{
    public class StatusService : IStatusService
    {
        public readonly IStatusRepository _statusRepository;

        public StatusService(IStatusRepository statusRepository)
        {
            _statusRepository = statusRepository;
        }

        public Status? GetStatusById(long id)
        {
            return _statusRepository.GetStatusById(id);
        }

        public List<Status> GetStatuses()
        {
            return _statusRepository.GetStatuses();
        }

        public bool Update(Status status)
        {
            return _statusRepository.Update(status);
        }
    }
}
