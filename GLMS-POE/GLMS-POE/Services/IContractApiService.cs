using GLMS_POE.Models;

namespace GLMS_POE.Services
{
    public interface IContractApiService
    {
        Task<List<Contract>> GetContractsAsync(DateTime? startDate = null, DateTime? endDate = null, string? status = null);
        Task<Contract?> GetContractByIdAsync(int id);
        Task<bool> CreateContractAsync(Contract contract);
        Task<bool> UpdateContractAsync(int id, Contract contract);
        Task<bool> UpdateContractStatusAsync(int id, string status);
        Task<bool> DeleteContractAsync(int id);
    }
}