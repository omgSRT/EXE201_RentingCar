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
    public class RoleRepository : IRoleRepository
    {
        private RoleDAO _roleDAO;
        public RoleRepository()
        {
            _roleDAO = new RoleDAO();
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
