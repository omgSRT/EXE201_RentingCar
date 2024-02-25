using BusinessObjects.Models;
using RentingCarDAO;
using RentingCarRepositories.RepositoryInterface;

namespace RentingCarRepositories.Repository
{
    public class StatusRepository : IStatusRepository
    {
        private StatusDAO _statusDAO;
        public StatusRepository()
        {
            _statusDAO = new StatusDAO();
        }
        public Status? GetStatusById(long id)
        {
            return _statusDAO.GetStatusById(id);
        }

        public List<Status> GetStatuses()
        {
            return _statusDAO.GetStatuses();
        }

        public bool Update(Status status)
        {
            return _statusDAO.Update(status);
        }
    }
}
