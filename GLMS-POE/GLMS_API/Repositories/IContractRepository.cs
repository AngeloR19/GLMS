using GLMS_API.Models;

namespace GLMS_API.Repositories
{
    public interface IContractRepository
    {
        Task<List<Contract>> GetAllAsync(DateTime? startDate, DateTime? endDate, string? status);
        Task<Contract?> GetByIdAsync(int id);
        Task<Contract> CreateAsync(Contract contract);
        Task<Contract?> UpdateAsync(int id, Contract contract);
        Task<Contract?> UpdateStatusAsync(int id, string status);
        Task<bool> DeleteAsync(int id);
    }
}