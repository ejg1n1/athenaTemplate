using AutoMapper;
using Core.Entities;
using Infrastructure.Data;

namespace Athena.Infrastructure.Repositories;

public class RolesRepository : Repository<ApplicationRole>, IRolesRepository
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public RolesRepository(ApplicationDbContext context, IMapper mapper) : base(context)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ApplicationRole?> QueryByName(string roleName)
    {
        return await _context.Roles.FirstOrDefaultAsync(r => r.Name == roleName);
    }
}