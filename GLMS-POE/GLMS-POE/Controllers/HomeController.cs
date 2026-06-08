using GLMS_POE.Models;
using GLMS_POE.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;

namespace GLMS_POE.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IClientApiService _clientApiService;
        private readonly IContractApiService _contractApiService;
        private readonly IServiceRequestApiService _serviceRequestApiService;

        public HomeController(
            IClientApiService clientApiService,
            IContractApiService contractApiService,
            IServiceRequestApiService serviceRequestApiService)
        {
            _clientApiService = clientApiService;
            _contractApiService = contractApiService;
            _serviceRequestApiService = serviceRequestApiService;
        }

        public async Task<IActionResult> Index()
        {
            var clients = await _clientApiService.GetClientsAsync();
            var contracts = await _contractApiService.GetContractsAsync();
            var serviceRequests = await _serviceRequestApiService.GetServiceRequestsAsync();

            var dashboard = new DashboardViewModel
            {
                TotalClients = clients.Count,
                TotalContracts = contracts.Count,
                TotalServiceRequests = serviceRequests.Count
            };

            return View(dashboard);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}