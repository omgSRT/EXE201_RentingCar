using BusinessObjects.Models;
using RentingCarDAO;
using RentingCarRepositories.RepositoryInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
