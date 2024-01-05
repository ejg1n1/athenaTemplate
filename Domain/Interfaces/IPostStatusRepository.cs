using Athena.Core.Interfaces;
using Core.Entities;

namespace Core.Interfaces;

public interface IPostStatusRepository : IRepository<PostStatus>
{
    Task<PostStatus?> QueryByName(string name);
    Task<List<PostStatus>> QueryAllWithNoTracking();
}