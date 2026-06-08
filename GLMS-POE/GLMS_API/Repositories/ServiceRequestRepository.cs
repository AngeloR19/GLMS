using GLMS_API.Data;
using GLMS_API.Models;
using Microsoft.EntityFrameworkCore;

namespace GLMS_API.Repositories
{
    public class ServiceRequestRepository : IServiceRequestRepository
    {
        private readonly AppDbContext _context;

        public ServiceRequestRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<ServiceRequest>> GetAllAsync()
        {
            return await _context.ServiceRequests
                .Include(s => s.Contract)
                .ThenInclude(c => c.Client)
                .ToListAsync();
        }

        public async Task<ServiceRequest?> GetByIdAsync(int id)
        {
            return await _context.ServiceRequests
                .Include(s => s.Contract)
                .ThenInclude(c => c.Client)
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<ServiceRequest> CreateAsync(ServiceRequest serviceRequest)
        {
            _context.ServiceRequests.Add(serviceRequest);
            await _context.SaveChangesAsync();
            return serviceRequest;
        }

        public async Task<ServiceRequest?> UpdateAsync(int id, ServiceRequest serviceRequest)
        {
            var existingRequest = await _context.ServiceRequests.FindAsync(id);

            if (existingRequest == null)
            {
                return null;
            }

            existingRequest.ContractId = serviceRequest.ContractId;
            existingRequest.Description = serviceRequest.Description;
            existingRequest.CurrencyCode = serviceRequest.CurrencyCode;
            existingRequest.CostForeign = serviceRequest.CostForeign;
            existingRequest.CostZAR = serviceRequest.CostZAR;
            existingRequest.Status = serviceRequest.Status;

            await _context.SaveChangesAsync();
            return existingRequest;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var serviceRequest = await _context.ServiceRequests.FindAsync(id);

            if (serviceRequest == null)
            {
                return false;
            }

            _context.ServiceRequests.Remove(serviceRequest);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}