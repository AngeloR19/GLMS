using GLMS_API.Models;

namespace GLMS_API.Repositories
{
    public interface IClientRepository
    {
        Task<List<Client>> GetAllAsync(string? search);
        Task<Client?> GetByIdAsync(int id);
        Task<Client> CreateAsync(Client client);
        Task<Client?> UpdateAsync(int id, Client client);
        Task<bool> DeleteAsync(int id);
    }
}