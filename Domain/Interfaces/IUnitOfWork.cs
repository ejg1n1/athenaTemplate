using Athena.Core.Interfaces;
using Microsoft.EntityFrameworkCore.Storage;

namespace Core.Interfaces;

public interface IUnitOfWork
{

    IUserRepository UserRepository { get; }
    IRolesRepository RolesRepository { get; }
    IAddressStatusRepository AddressStatusRepository { get;  }
    IPostStatusRepository PostStatusRepository { get; }
    IPostRepository PostRepository { get; }

    bool IsModified<T>(T entity);
    Task<IDbContextTransaction> BeginDataBaseTransaction();
    Task<bool> CompleteAsync();
}
