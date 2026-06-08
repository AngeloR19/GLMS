using GLMS_API.Models;
using GLMS_API.Repositories;

namespace GLMS_API.Services
{
    public class ClientService : IClientService
    {
        private readonly IClientRepository _clientRepository;

        public ClientService(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        public async Task<List<Client>> GetClientsAsync(string? search)
        {
            return await _clientRepository.GetAllAsync(search);
        }

        public async Task<Client?> GetClientByIdAsync(int id)
        {
            return await _clientRepository.GetByIdAsync(id);
        }

        public async Task<Client> CreateClientAsync(Client client)
        {
            return await _clientRepository.CreateAsync(client);
        }

        public async Task<Client?> UpdateClientAsync(int id, Client client)
        {
            return await _clientRepository.UpdateAsync(id, client);
        }

        public async Task<bool> DeleteClientAsync(int id)
        {
            return await _clientRepository.DeleteAsync(id);
        }
    }
}