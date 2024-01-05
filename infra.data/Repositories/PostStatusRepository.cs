using Athena.Infrastructure.Repositories;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;

namespace Infrastructure.Repositories;

public class PostStatusRepository : Repository<PostStatus>, IPostStatusRepository
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;
    public PostStatusRepository(ApplicationDbContext context, IMapper mapper) : base(context)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PostStatus?> QueryByName(string name)
    {
        return await _context.PostStatuses.FirstOrDefaultAsync(d => d.Description == name);
    }

    public async Task<List<PostStatus>> QueryAllWithNoTracking()
    {
        return await _context.PostStatuses.AsNoTracking().ToListAsync();
    }
}