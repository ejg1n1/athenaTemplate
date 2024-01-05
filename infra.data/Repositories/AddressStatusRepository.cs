using Athena.Infrastructure.Repositories;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;

namespace Infrastructure.Repositories;

public class AddressStatusRepository : Repository<AddressStatus>, IAddressStatusRepository
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public AddressStatusRepository(ApplicationDbContext context, IMapper mapper) : base(context)
    {
        _mapper = mapper;
        _context = context;
    }

    public async Task<AddressStatus?> QueryByName(string name)
    {
        return await _context.AddressStatuses.FirstOrDefaultAsync(d => d.Description == name);
    }

    public async Task<List<AddressStatus>> QueryAllWithNoTracking()
    {
        return await _context.AddressStatuses.AsNoTracking().ToListAsync();
    }
}