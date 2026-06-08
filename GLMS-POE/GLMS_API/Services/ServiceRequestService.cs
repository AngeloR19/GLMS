using GLMS_API.Data;
using GLMS_API.Models;
using GLMS_API.Repositories;
using Microsoft.EntityFrameworkCore;

namespace GLMS_API.Services
{
    public class ServiceRequestService : IServiceRequestService
    {
        private readonly IServiceRequestRepository _serviceRequestRepository;
        private readonly AppDbContext _context;

        public ServiceRequestService(
            IServiceRequestRepository serviceRequestRepository,
            AppDbContext context)
        {
            _serviceRequestRepository = serviceRequestRepository;
            _context = context;
        }

        public async Task<List<ServiceRequest>> GetServiceRequestsAsync()
        {
            return await _serviceRequestRepository.GetAllAsync();
        }

        public async Task<ServiceRequest?> GetServiceRequestByIdAsync(int id)
        {
            return await _serviceRequestRepository.GetByIdAsync(id);
        }

        public async Task<ServiceRequest> CreateServiceRequestAsync(ServiceRequest serviceRequest)
        {
            var contract = await _context.Contracts.FirstOrDefaultAsync(c => c.Id == serviceRequest.ContractId);

            if (contract == null)
            {
                throw new ArgumentException("Contract not found.");
            }

            if (contract.Status == "Expired" || contract.Status == "On Hold" || contract.Status == "Declined")
            {
                throw new ArgumentException("Service request cannot be created for an inactive contract.");
            }

            serviceRequest.CostZAR = await CurrencyService.Instance.ConvertToZAR(
                serviceRequest.CurrencyCode,
                serviceRequest.CostForeign
            );

            return await _serviceRequestRepository.CreateAsync(serviceRequest);
        }

        public async Task<ServiceRequest?> UpdateServiceRequestAsync(int id, ServiceRequest serviceRequest)
        {
            serviceRequest.CostZAR = await CurrencyService.Instance.ConvertToZAR(
                serviceRequest.CurrencyCode,
                serviceRequest.CostForeign
            );

            return await _serviceRequestRepository.UpdateAsync(id, serviceRequest);
        }

        public async Task<bool> DeleteServiceRequestAsync(int id)
        {
            return await _serviceRequestRepository.DeleteAsync(id);
        }
    }
}