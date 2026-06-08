using GLMS_API.Models;

namespace GLMS_API.Repositories
{
    public interface IServiceRequestRepository
    {
        Task<List<ServiceRequest>> GetAllAsync();
        Task<ServiceRequest?> GetByIdAsync(int id);
        Task<ServiceRequest> CreateAsync(ServiceRequest serviceRequest);
        Task<ServiceRequest?> UpdateAsync(int id, ServiceRequest serviceRequest);
        Task<bool> DeleteAsync(int id);
    }
}