using GLMS_POE.Models;
using GLMS_POE.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GLMS_POE.Controllers
{
    [Authorize]
    public class ServiceRequestController : Controller
    {
        private readonly IServiceRequestApiService _serviceRequestApiService;
        private readonly IContractApiService _contractApiService;

        public ServiceRequestController(
            IServiceRequestApiService serviceRequestApiService,
            IContractApiService contractApiService)
        {
            _serviceRequestApiService = serviceRequestApiService;
            _contractApiService = contractApiService;
        }

        public async Task<IActionResult> Index()
        {
            var requests = await _serviceRequestApiService.GetServiceRequestsAsync();
            return View(requests);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var serviceRequest = await _serviceRequestApiService.GetServiceRequestByIdAsync(id.Value);

            if (serviceRequest == null) return NotFound();

            return View(serviceRequest);
        }

        public async Task<IActionResult> Create()
        {
            await LoadContracts();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ServiceRequest serviceRequest)
        {
            if (!ModelState.IsValid)
            {
                await LoadContracts(serviceRequest.ContractId);
                return View(serviceRequest);
            }

            var success = await _serviceRequestApiService.CreateServiceRequestAsync(serviceRequest);

            if (!success)
            {
                ModelState.AddModelError("", "Unable to create service request. The contract may be inactive or the API may not be running.");
                await LoadContracts(serviceRequest.ContractId);
                return View(serviceRequest);
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var serviceRequest = await _serviceRequestApiService.GetServiceRequestByIdAsync(id.Value);

            if (serviceRequest == null) return NotFound();

            await LoadContracts(serviceRequest.ContractId);
            return View(serviceRequest);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ContractId,Description,CurrencyCode,CostForeign,CostZAR,Status")] ServiceRequest serviceRequest)
        {
            if (id != serviceRequest.Id) return NotFound();

            if (!ModelState.IsValid)
            {
                await LoadContracts(serviceRequest.ContractId);
                return View(serviceRequest);
            }

            var success = await _serviceRequestApiService.UpdateServiceRequestAsync(id, serviceRequest);

            if (!success)
            {
                ModelState.AddModelError("", "Unable to update service request. Please check that the API is running.");
                await LoadContracts(serviceRequest.ContractId);
                return View(serviceRequest);
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var serviceRequest = await _serviceRequestApiService.GetServiceRequestByIdAsync(id.Value);

            if (serviceRequest == null) return NotFound();

            return View(serviceRequest);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var success = await _serviceRequestApiService.DeleteServiceRequestAsync(id);

            if (!success)
            {
                return NotFound();
            }

            return RedirectToAction(nameof(Index));
        }

        private async Task LoadContracts(object? selected = null)
        {
            var contracts = await _contractApiService.GetContractsAsync();

            var contractList = contracts.Select(c => new
            {
                c.Id,
                Display = $"{(c.ClientName ?? c.Client?.Name ?? "Unknown Client")} - {c.ServiceLevel}"
            });

            ViewData["ContractId"] = new SelectList(
                contractList,
                "Id",
                "Display",
                selected
            );
        }
    }
}
/////////////////////////////////////////// ----- End Of File ----- ///////////////////////////////////////////