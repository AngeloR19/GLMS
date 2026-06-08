using GLMS_API.Models;
using GLMS_API.Repositories;

namespace GLMS_API.Services
{
    public class ContractService : IContractService
    {
        private readonly IContractRepository _contractRepository;

        public ContractService(IContractRepository contractRepository)
        {
            _contractRepository = contractRepository;
        }

        public async Task<List<Contract>> GetContractsAsync(DateTime? startDate, DateTime? endDate, string? status)
        {
            return await _contractRepository.GetAllAsync(startDate, endDate, status);
        }

        public async Task<Contract?> GetContractByIdAsync(int id)
        {
            return await _contractRepository.GetByIdAsync(id);
        }

        public async Task<Contract> CreateContractAsync(Contract contract)
        {
            if (contract.EndDate < contract.StartDate)
            {
                throw new ArgumentException("End date cannot be before start date.");
            }

            return await _contractRepository.CreateAsync(contract);
        }

        public async Task<Contract?> UpdateContractAsync(int id, Contract contract)
        {
            if (contract.EndDate < contract.StartDate)
            {
                throw new ArgumentException("End date cannot be before start date.");
            }

            return await _contractRepository.UpdateAsync(id, contract);
        }

        public async Task<Contract?> UpdateContractStatusAsync(int id, string status)
        {
            if (status != "Approved" && status != "Declined" && status != "Active" && status != "Expired" && status != "On Hold")
            {
                throw new ArgumentException("Invalid contract status.");
            }

            return await _contractRepository.UpdateStatusAsync(id, status);
        }

        public async Task<bool> DeleteContractAsync(int id)
        {
            return await _contractRepository.DeleteAsync(id);
        }
    }
}