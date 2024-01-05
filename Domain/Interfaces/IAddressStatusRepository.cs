using Athena.Core.Interfaces;
using Core.Entities;

namespace Core.Interfaces;

public interface IAddressStatusRepository : IRepository<AddressStatus>
{
    Task<AddressStatus?> QueryByName(string name);
    Task<List<AddressStatus>> QueryAllWithNoTracking();
    
}