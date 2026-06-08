using GLMS_POE.Models;
using GLMS_POE.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;

namespace GLMS_POE.Controllers
{
    [Authorize]
    public class ContractController : Controller
    {
        private readonly IContractApiService _contractApiService;
        private readonly IClientApiService _clientApiService;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ContractController(
            IContractApiService contractApiService,
            IClientApiService clientApiService,
            IWebHostEnvironment webHostEnvironment)
        {
            _contractApiService = contractApiService;
            _clientApiService = clientApiService;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<IActionResult> Index(DateTime? startDate, DateTime? endDate, string? status)
        {
            var contracts = await _contractApiService.GetContractsAsync(startDate, endDate, status);
            return View(contracts);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var contract = await _contractApiService.GetContractByIdAsync(id.Value);

            if (contract == null) return NotFound();

            return View(contract);
        }

        public async Task<IActionResult> Create()
        {
            await LoadClients();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Contract contract, IFormFile file)
        {
            if (!ModelState.IsValid)
            {
                await LoadClients(contract.ClientId);
                return View(contract);
            }

            if (file != null && file.Length > 0)
            {
                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "uploads/contracts");

                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                string uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                contract.FilePath = "/uploads/contracts/" + uniqueFileName;
            }
            else
            {
                contract.FilePath ??= "";
            }

            var success = await _contractApiService.CreateContractAsync(contract);

            if (!success)
            {
                ModelState.AddModelError("", "Unable to create contract. Please check that the API is running.");
                await LoadClients(contract.ClientId);
                return View(contract);
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var contract = await _contractApiService.GetContractByIdAsync(id.Value);

            if (contract == null) return NotFound();

            await LoadClients(contract.ClientId);
            return View(contract);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Contract contract)
        {
            if (id != contract.Id) return NotFound();

            if (!ModelState.IsValid)
            {
                await LoadClients(contract.ClientId);
                return View(contract);
            }

            var success = await _contractApiService.UpdateContractAsync(id, contract);

            if (!success)
            {
                ModelState.AddModelError("", "Unable to update contract. Please check that the API is running.");
                await LoadClients(contract.ClientId);
                return View(contract);
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var contract = await _contractApiService.GetContractByIdAsync(id.Value);

            if (contract == null) return NotFound();

            return View(contract);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var success = await _contractApiService.DeleteContractAsync(id);

            if (!success)
            {
                return NotFound();
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Approve(int id)
        {
            var success = await _contractApiService.UpdateContractStatusAsync(id, "Approved");

            if (!success)
            {
                return NotFound();
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Decline(int id)
        {
            var success = await _contractApiService.UpdateContractStatusAsync(id, "Declined");

            if (!success)
            {
                return NotFound();
            }

            return RedirectToAction(nameof(Index));
        }

        private async Task LoadClients(object? selected = null)
        {
            var clients = await _clientApiService.GetClientsAsync();

            ViewData["ClientId"] = new SelectList(
                clients,
                "Id",
                "Name",
                selected
            );
        }

        public IActionResult Download(string filePath)
        {
            string fullPath = Path.Combine(
                Directory.GetCurrentDirectory(),
                "wwwroot",
                filePath.TrimStart('/')
            );

            if (!System.IO.File.Exists(fullPath))
            {
                return NotFound();
            }

            byte[] fileBytes = System.IO.File.ReadAllBytes(fullPath);
            string fileName = Path.GetFileName(fullPath);

            return File(fileBytes, "application/pdf", fileName);
        }
    }
}
/////////////////////////////////////////// ----- End Of File ----- ///////////////////////////////////////////