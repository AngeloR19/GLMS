using GLMS_API.Models;

namespace GLMS_API.Services
{
    public interface IServiceRequestService
    {
        Task<List<ServiceRequest>> GetServiceRequestsAsync();
        Task<ServiceRequest?> GetServiceRequestByIdAsync(int id);
        Task<ServiceRequest> CreateServiceRequestAsync(ServiceRequest serviceRequest);
        Task<ServiceRequest?> UpdateServiceRequestAsync(int id, ServiceRequest serviceRequest);
        Task<bool> DeleteServiceRequestAsync(int id);
    }
}