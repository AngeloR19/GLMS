using GLMS_POE.Models;
using System.Net.Http.Json;

namespace GLMS_POE.Services
{
    public class ClientApiService : IClientApiService
    {
        private readonly HttpClient _httpClient;

        public ClientApiService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("GLMS_API");
        }

        public async Task<List<Client>> GetClientsAsync(string? search = null)
        {
            string endpoint = "api/clients";

            if (!string.IsNullOrEmpty(search))
            {
                endpoint += $"?search={Uri.EscapeDataString(search)}";
            }

            var clients = await _httpClient.GetFromJsonAsync<List<Client>>(endpoint);

            return clients ?? new List<Client>();
        }

        public async Task<Client?> GetClientByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<Client>($"api/clients/{id}");
        }

        public async Task<bool> CreateClientAsync(Client client)
        {
            var response = await _httpClient.PostAsJsonAsync("api/clients", client);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateClientAsync(int id, Client client)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/clients/{id}", client);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteClientAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/clients/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}