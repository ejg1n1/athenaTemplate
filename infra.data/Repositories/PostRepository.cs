using Athena.Infrastructure.Repositories;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;

namespace Infrastructure.Repositories;

public class PostRepository : Repository<Post>, IPostRepository
{
    private readonly IMapper _mapper;
    private readonly ApplicationDbContext _context;
    
    public PostRepository(ApplicationDbContext context, IMapper mapper) : base(context)
    {
        _mapper = mapper;
        _context = context;
    }
}