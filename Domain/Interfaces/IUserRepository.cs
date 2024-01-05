using Core.Entities;

namespace Athena.Core.Interfaces;

public interface IUserRepository : IRepository<ApplicationUser>
{
    Task<ApplicationUser?> QueryInclUserRoles(string emailAddress);
    Task<ApplicationUser?> QueryInclUserRoles(Guid userId);
    Task<ApplicationUser?> QueryByUserName(string username);
}   