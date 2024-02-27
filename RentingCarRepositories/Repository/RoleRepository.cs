using BusinessObjects.Models;
using RentingCarDAO;
using RentingCarRepositories.RepositoryInterface;

namespace RentingCarRepositories.Repository
{
    public class RoleRepository : IRoleRepository
    {
        private RoleDAO _roleDAO;
        public RoleRepository()
        {
            _roleDAO = new RoleDAO();
        }

        public bool Add(Role role)
        {
            return _roleDAO.Add(role);
        }

        public Role? GetRoleById(long id)
        {
            return _roleDAO.GetRoleById(id);
        }

        public List<Role> GetRoles()
        {
            return _roleDAO.GetRoles();
        }

        public bool Update(Role role)
        {
            return _roleDAO.Update(role);
        }
    }
}
