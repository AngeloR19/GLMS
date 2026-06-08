using GLMS_POE.Models;

namespace GLMS_POE.Services
{
    public interface IServiceRequestApiService
    {
        Task<List<ServiceRequest>> GetServiceRequestsAsync();
        Task<ServiceRequest?> GetServiceRequestByIdAsync(int id);
        Task<bool> CreateServiceRequestAsync(ServiceRequest serviceRequest);
        Task<bool> UpdateServiceRequestAsync(int id, ServiceRequest serviceRequest);
        Task<bool> DeleteServiceRequestAsync(int id);
    }
}