using AutoMapper;
using Core.Entities;
using Infrastructure.Data;

namespace Athena.Infrastructure.Repositories;

public class UserRepository : Repository<ApplicationUser>, IUserRepository
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;
    
    public UserRepository(ApplicationDbContext context, IMapper mapper) : base(context)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ApplicationUser?> QueryInclUserRoles(string emailAddress)
    {
        return await _context
            .Users
            .Include(u => u.UserRoles).ThenInclude(ur => ur.Role)
            .FirstOrDefaultAsync(u => u.NormalizedEmail == emailAddress.ToUpper());
    }

    public async Task<ApplicationUser?> QueryInclUserRoles(Guid userId)
    {
        return await _context
            .Users
            .Include(u => u.UserRoles)
            .ThenInclude(ur => ur.Role)
            .FirstOrDefaultAsync(u => u.Id == userId);
    }

    public async Task<ApplicationUser?> QueryByUserName(string username)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.UserName == username);
    }
}