using GLMS_API.Models;

namespace GLMS_API.Services
{
    public interface IClientService
    {
        Task<List<Client>> GetClientsAsync(string? search);
        Task<Client?> GetClientByIdAsync(int id);
        Task<Client> CreateClientAsync(Client client);
        Task<Client?> UpdateClientAsync(int id, Client client);
        Task<bool> DeleteClientAsync(int id);
    }
}