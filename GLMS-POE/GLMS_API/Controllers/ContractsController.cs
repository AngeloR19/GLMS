using GLMS_API.Models;
using GLMS_API.Services;
using GLMS_API.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace GLMS_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContractsController : ControllerBase
    {
        private readonly IContractService _contractService;

        public ContractsController(IContractService contractService)
        {
            _contractService = contractService;
        }

        [HttpGet]
        public async Task<IActionResult> GetContracts(
            [FromQuery] DateTime? startDate,
            [FromQuery] DateTime? endDate,
            [FromQuery] string? status)
        {
            var contracts = await _contractService.GetContractsAsync(startDate, endDate, status);

            var contractDtos = contracts.Select(c => new ContractDto
            {
                Id = c.Id,
                ClientId = c.ClientId,
                ClientName = c.Client?.Name ?? "",
                StartDate = c.StartDate,
                EndDate = c.EndDate,
                Status = c.Status,
                ServiceLevel = c.ServiceLevel,
                FilePath = c.FilePath
            });

            return Ok(contractDtos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetContract(int id)
        {
            var contract = await _contractService.GetContractByIdAsync(id);

            if (contract == null)
            {
                return NotFound();
            }

            var dto = new ContractDto
            {
                Id = contract.Id,
                ClientId = contract.ClientId,
                ClientName = contract.Client?.Name ?? "",
                StartDate = contract.StartDate,
                EndDate = contract.EndDate,
                Status = contract.Status,
                ServiceLevel = contract.ServiceLevel,
                FilePath = contract.FilePath
            };

            return Ok(dto);
        }

        [HttpPost]
        public async Task<IActionResult> CreateContract([FromBody] Contract contract)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var createdContract = await _contractService.CreateContractAsync(contract);

                return CreatedAtAction(
                    nameof(GetContract),
                    new { id = createdContract.Id },
                    createdContract
                );
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateContract(int id, [FromBody] Contract contract)
        {
            if (id != contract.Id)
            {
                return BadRequest("Contract ID mismatch.");
            }

            try
            {
                var updatedContract = await _contractService.UpdateContractAsync(id, contract);

                if (updatedContract == null)
                {
                    return NotFound();
                }

                return Ok(updatedContract);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPatch("{id}/status")]
        public async Task<IActionResult> UpdateContractStatus(int id, [FromBody] string status)
        {
            try
            {
                var updatedContract = await _contractService.UpdateContractStatusAsync(id, status);

                if (updatedContract == null)
                {
                    return NotFound();
                }

                return Ok(updatedContract);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContract(int id)
        {
            var deleted = await _contractService.DeleteContractAsync(id);

            if (!deleted)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}