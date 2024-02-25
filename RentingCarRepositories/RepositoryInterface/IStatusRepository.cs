using BusinessObjects.Models;

namespace RentingCarRepositories.RepositoryInterface
{
    public interface IStatusRepository
    {
        List<Status> GetStatuses();
        Status? GetStatusById(long id);
        bool Update(Status status);
    }
}
