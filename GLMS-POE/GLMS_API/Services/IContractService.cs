using GLMS_API.Models;

namespace GLMS_API.Services
{
    public interface IContractService
    {
        Task<List<Contract>> GetContractsAsync(DateTime? startDate, DateTime? endDate, string? status);
        Task<Contract?> GetContractByIdAsync(int id);
        Task<Contract> CreateContractAsync(Contract contract);
        Task<Contract?> UpdateContractAsync(int id, Contract contract);
        Task<Contract?> UpdateContractStatusAsync(int id, string status);
        Task<bool> DeleteContractAsync(int id);
    }
}
