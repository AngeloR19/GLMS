using GLMS_POE.Models;
using System.Net.Http.Json;

namespace GLMS_POE.Services
{
    public class ServiceRequestApiService : IServiceRequestApiService
    {
        private readonly HttpClient _httpClient;

        public ServiceRequestApiService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("GLMS_API");
        }

        public async Task<List<ServiceRequest>> GetServiceRequestsAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<ServiceRequest>>("api/servicerequests")
                   ?? new List<ServiceRequest>();
        }

        public async Task<ServiceRequest?> GetServiceRequestByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<ServiceRequest>($"api/servicerequests/{id}");
        }

        public async Task<bool> CreateServiceRequestAsync(ServiceRequest serviceRequest)
        {
            var response = await _httpClient.PostAsJsonAsync("api/servicerequests", serviceRequest);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateServiceRequestAsync(int id, ServiceRequest serviceRequest)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/servicerequests/{id}", serviceRequest);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteServiceRequestAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/servicerequests/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}