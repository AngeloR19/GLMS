using GLMS_POE.Models;
using GLMS_POE.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace GLMS_POE.Controllers
{
    [Authorize]
    public class ClientController : Controller
    {
        private readonly IClientApiService _clientApiService;

        public ClientController(IClientApiService clientApiService)
        {
            _clientApiService = clientApiService;
        }

        // GET: Client
        public async Task<IActionResult> Index(string search)
        {
            var clients = await _clientApiService.GetClientsAsync(search);
            return View(clients);
        }

        // GET: Client/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var client = await _clientApiService.GetClientByIdAsync(id.Value);

            if (client == null) return NotFound();

            return View(client);
        }

        // GET: Client/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Client/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Email,PhoneNumber,Region")] Client client)
        {
            if (!ModelState.IsValid)
            {
                return View(client);
            }

            var success = await _clientApiService.CreateClientAsync(client);

            if (!success)
            {
                ModelState.AddModelError("", "Unable to create client. Please check that the API is running.");
                return View(client);
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: Client/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var client = await _clientApiService.GetClientByIdAsync(id.Value);

            if (client == null) return NotFound();

            return View(client);
        }

        // POST: Client/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Email,PhoneNumber,Region")] Client client)
        {
            if (id != client.Id) return NotFound();

            if (!ModelState.IsValid)
            {
                return View(client);
            }

            var success = await _clientApiService.UpdateClientAsync(id, client);

            if (!success)
            {
                ModelState.AddModelError("", "Unable to update client. Please check that the API is running.");
                return View(client);
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: Client/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var client = await _clientApiService.GetClientByIdAsync(id.Value);

            if (client == null) return NotFound();

            return View(client);
        }

        // POST: Client/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var success = await _clientApiService.DeleteClientAsync(id);

            if (!success)
            {
                return NotFound();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
/////////////////////////////////////////// ----- End Of File ----- ///////////////////////////////////////////