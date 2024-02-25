using BusinessObjects.Models;

namespace RentingCarRepositories.RepositoryInterface
{
    public interface IRoleRepository
    {
        List<Role> GetRoles();
        Role? GetRoleById(long id);
        bool Add(Role role);
        bool Update(Role role);
    }
}
