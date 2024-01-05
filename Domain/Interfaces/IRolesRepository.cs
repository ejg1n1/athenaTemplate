using Core.Entities;

namespace Athena.Core.Interfaces;

public interface IRolesRepository : IRepository<ApplicationRole>
{
    Task<ApplicationRole?> QueryByName(string roleName);
}