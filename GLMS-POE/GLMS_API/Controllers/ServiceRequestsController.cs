using GLMS_API.DTOs;
using GLMS_API.Models;
using GLMS_API.Services;
using Microsoft.AspNetCore.Mvc;

namespace GLMS_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceRequestsController : ControllerBase
    {
        private readonly IServiceRequestService _serviceRequestService;

        public ServiceRequestsController(IServiceRequestService serviceRequestService)
        {
            _serviceRequestService = serviceRequestService;
        }

        [HttpGet]
        public async Task<IActionResult> GetServiceRequests()
        {
            var requests = await _serviceRequestService.GetServiceRequestsAsync();

            var requestDtos = requests.Select(r => new ServiceRequestDto
            {
                Id = r.Id,
                ContractId = r.ContractId,
                ClientName = r.Contract?.Client?.Name ?? "",
                Description = r.Description,
                CurrencyCode = r.CurrencyCode,
                CostForeign = r.CostForeign,
                CostZAR = r.CostZAR,
                Status = r.Status
            });

            return Ok(requestDtos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetServiceRequest(int id)
        {
            var request = await _serviceRequestService.GetServiceRequestByIdAsync(id);

            if (request == null)
            {
                return NotFound();
            }

            var dto = new ServiceRequestDto
            {
                Id = request.Id,
                ContractId = request.ContractId,
                ClientName = request.Contract?.Client?.Name ?? "",
                Description = request.Description,
                CurrencyCode = request.CurrencyCode,
                CostForeign = request.CostForeign,
                CostZAR = request.CostZAR,
                Status = request.Status
            };

            return Ok(dto);
        }

        [HttpPost]
        public async Task<IActionResult> CreateServiceRequest([FromBody] ServiceRequest serviceRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var createdRequest = await _serviceRequestService.CreateServiceRequestAsync(serviceRequest);

                return CreatedAtAction(
                    nameof(GetServiceRequest),
                    new { id = createdRequest.Id },
                    createdRequest
                );
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateServiceRequest(int id, [FromBody] ServiceRequest serviceRequest)
        {
            if (id != serviceRequest.Id)
            {
                return BadRequest("Service Request ID mismatch.");
            }

            try
            {
                var updatedRequest = await _serviceRequestService.UpdateServiceRequestAsync(id, serviceRequest);

                if (updatedRequest == null)
                {
                    return NotFound();
                }

                return Ok(updatedRequest);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteServiceRequest(int id)
        {
            var deleted = await _serviceRequestService.DeleteServiceRequestAsync(id);

            if (!deleted)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}