using System.Data;

namespace Clients.Application;

public interface IClientRepository
{
    Task<bool> Exists(Guid id);
    Task Add(Client client);
}