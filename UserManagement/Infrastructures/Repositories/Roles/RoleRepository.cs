
using Application.Interfaces.IRepositories;

namespace Infrastructures.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly UserManagementDbContext _context;

        public RoleRepository(UserManagementDbContext context)
        {
            _context = context;
        }

        public string GetRoleName(int roleid)
        {
            var role = _context.Roles.Where(r=>r.Id == roleid).FirstOrDefault();
            return role.Name;
        }
    }
}
