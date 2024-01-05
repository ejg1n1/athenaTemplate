using Athena.Infrastructure.Repositories;
using AutoMapper;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore.Storage;

namespace Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;
    
    private readonly Lazy<IUserRepository> _usersRepository;
    private readonly Lazy<IRolesRepository> _rolesRepository;
    private readonly Lazy<IAddressStatusRepository> _addressStatusRepository;
    private readonly Lazy<IPostStatusRepository> _postStatusRepository;
    private readonly Lazy<IPostRepository> _postRepository;

    public UnitOfWork(ApplicationDbContext context, IMapper mapper)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        
        _usersRepository = new Lazy<IUserRepository>(() => new UserRepository(_context, _mapper), true);
        _rolesRepository = new Lazy<IRolesRepository>(() => new RolesRepository(context, mapper), true);
        _addressStatusRepository = new Lazy<IAddressStatusRepository>(() => new AddressStatusRepository(context, mapper), true);
        _postStatusRepository = new Lazy<IPostStatusRepository>(() => new PostStatusRepository(context, mapper), true);
        _postRepository = new Lazy<IPostRepository>(() => new PostRepository(context, mapper), true);
    }

    public IUserRepository UserRepository => _usersRepository.Value;
    public IRolesRepository RolesRepository => _rolesRepository.Value;
    public IAddressStatusRepository AddressStatusRepository => _addressStatusRepository.Value;
    public IPostStatusRepository PostStatusRepository => _postStatusRepository.Value;
    public IPostRepository PostRepository => _postRepository.Value;
    
    public bool IsModified<T>(T entity)
    {
        return entity != null && _context.Entry(entity).State == EntityState.Modified;
    }

    public async Task<IDbContextTransaction> BeginDataBaseTransaction()
    {
        return await _context.Database.BeginTransactionAsync();
    }

    public async Task<bool> CompleteAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }
}
