using GLMS_POE.Models;
using System.Net.Http.Json;

namespace GLMS_POE.Services
{
    public class ContractApiService : IContractApiService
    {
        private readonly HttpClient _httpClient;

        public ContractApiService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("GLMS_API");
        }

        public async Task<List<Contract>> GetContractsAsync(DateTime? startDate = null, DateTime? endDate = null, string? status = null)
        {
            var query = new List<string>();

            if (startDate.HasValue)
                query.Add($"startDate={startDate.Value:yyyy-MM-dd}");

            if (endDate.HasValue)
                query.Add($"endDate={endDate.Value:yyyy-MM-dd}");

            if (!string.IsNullOrEmpty(status))
                query.Add($"status={Uri.EscapeDataString(status)}");

            var endpoint = "api/contracts";

            if (query.Any())
                endpoint += "?" + string.Join("&", query);

            return await _httpClient.GetFromJsonAsync<List<Contract>>(endpoint) ?? new List<Contract>();
        }

        public async Task<Contract?> GetContractByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<Contract>($"api/contracts/{id}");
        }

        public async Task<bool> CreateContractAsync(Contract contract)
        {
            var response = await _httpClient.PostAsJsonAsync("api/contracts", contract);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateContractAsync(int id, Contract contract)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/contracts/{id}", contract);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateContractStatusAsync(int id, string status)
        {
            var response = await _httpClient.PatchAsJsonAsync($"api/contracts/{id}/status", status);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteContractAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/contracts/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}