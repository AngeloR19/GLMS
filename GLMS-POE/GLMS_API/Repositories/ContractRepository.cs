using GLMS_API.Data;
using GLMS_API.Models;
using Microsoft.EntityFrameworkCore;

namespace GLMS_API.Repositories
{
    public class ContractRepository : IContractRepository
    {
        private readonly AppDbContext _context;

        public ContractRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Contract>> GetAllAsync(DateTime? startDate, DateTime? endDate, string? status)
        {
            var contracts = _context.Contracts
                .Include(c => c.Client)
                .AsQueryable();

            if (!string.IsNullOrEmpty(status))
            {
                contracts = contracts.Where(c => c.Status == status);
            }

            if (startDate.HasValue)
            {
                contracts = contracts.Where(c => c.StartDate >= startDate.Value);
            }

            if (endDate.HasValue)
            {
                contracts = contracts.Where(c => c.EndDate <= endDate.Value);
            }

            return await contracts.ToListAsync();
        }

        public async Task<Contract?> GetByIdAsync(int id)
        {
            return await _context.Contracts
                .Include(c => c.Client)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Contract> CreateAsync(Contract contract)
        {
            _context.Contracts.Add(contract);
            await _context.SaveChangesAsync();
            return contract;
        }

        public async Task<Contract?> UpdateAsync(int id, Contract contract)
        {
            var existingContract = await _context.Contracts.FindAsync(id);

            if (existingContract == null)
            {
                return null;
            }

            existingContract.ClientId = contract.ClientId;
            existingContract.StartDate = contract.StartDate;
            existingContract.EndDate = contract.EndDate;
            existingContract.Status = contract.Status;
            existingContract.ServiceLevel = contract.ServiceLevel;
            existingContract.FilePath = contract.FilePath;

            await _context.SaveChangesAsync();
            return existingContract;
        }

        public async Task<Contract?> UpdateStatusAsync(int id, string status)
        {
            var contract = await _context.Contracts.FindAsync(id);

            if (contract == null)
            {
                return null;
            }

            contract.Status = status;
            await _context.SaveChangesAsync();

            return contract;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var contract = await _context.Contracts.FindAsync(id);

            if (contract == null)
            {
                return false;
            }

            _context.Contracts.Remove(contract);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}