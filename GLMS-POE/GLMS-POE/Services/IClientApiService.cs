using GLMS_POE.Models;

namespace GLMS_POE.Services
{
    public interface IClientApiService
    {
        Task<List<Client>> GetClientsAsync(string? search = null);
        Task<Client?> GetClientByIdAsync(int id);
        Task<bool> CreateClientAsync(Client client);
        Task<bool> UpdateClientAsync(int id, Client client);
        Task<bool> DeleteClientAsync(int id);
    }
}