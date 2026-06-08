using GLMS_API.Data;
using GLMS_API.Models;
using Microsoft.EntityFrameworkCore;

namespace GLMS_API.Repositories
{
    public class ClientRepository : IClientRepository
    {
        private readonly AppDbContext _context;

        public ClientRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Client>> GetAllAsync(string? search)
        {
            var clients = _context.Clients.AsQueryable();

            if (!string.IsNullOrEmpty(search))
            {
                clients = clients.Where(c => c.Name.Contains(search));
            }

            return await clients.ToListAsync();
        }

        public async Task<Client?> GetByIdAsync(int id)
        {
            return await _context.Clients.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Client> CreateAsync(Client client)
        {
            _context.Clients.Add(client);
            await _context.SaveChangesAsync();
            return client;
        }

        public async Task<Client?> UpdateAsync(int id, Client client)
        {
            var existingClient = await _context.Clients.FindAsync(id);

            if (existingClient == null)
            {
                return null;
            }

            existingClient.Name = client.Name;
            existingClient.Email = client.Email;
            existingClient.PhoneNumber = client.PhoneNumber;
            existingClient.Region = client.Region;

            await _context.SaveChangesAsync();
            return existingClient;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var client = await _context.Clients.FindAsync(id);

            if (client == null)
            {
                return false;
            }

            _context.Clients.Remove(client);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}