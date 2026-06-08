using NUnit.Framework;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;

namespace GLMS_POE_Tests
{
    public class ApiIntegrationTests
    {
        private HttpClient _client = null!;

        [SetUp]
        public void Setup()
        {
            _client = new HttpClient
            {
                BaseAddress = new Uri("http://localhost:5000/")
            };
        }

        [TearDown]
        public void TearDown()
        {
            _client.Dispose();
        }

        [Test]
        public async Task GetContracts_ShouldReturnOk_AndJsonNotNull()
        {
            var response = await _client.GetAsync("api/Contracts");

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));

            var json = await response.Content.ReadAsStringAsync();

            Assert.That(json, Is.Not.Null);
            Assert.That(json, Is.Not.Empty);
        }

        [Test]
        public async Task GetClients_ShouldReturnOk_AndJsonNotNull()
        {
            var response = await _client.GetAsync("api/Clients");

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));

            var json = await response.Content.ReadAsStringAsync();

            Assert.That(json, Is.Not.Null);
            Assert.That(json, Is.Not.Empty);
        }

        [Test]
        public async Task GetServiceRequests_ShouldReturnOk_AndJsonNotNull()
        {
            var response = await _client.GetAsync("api/ServiceRequests");

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));

            var json = await response.Content.ReadAsStringAsync();

            Assert.That(json, Is.Not.Null);
        }

        [Test]
        public async Task CreateClient_ThenReadClient_ShouldVerifyDataIntegrity()
        {
            var newClient = new
            {
                name = "Integration Test Client",
                email = $"test{Guid.NewGuid()}@client.co.za",
                phoneNumber = "0215559999",
                region = "Western Cape"
            };

            var createResponse = await _client.PostAsJsonAsync("api/Clients", newClient);

            Assert.That(createResponse.StatusCode, Is.EqualTo(HttpStatusCode.Created));

            var createJson = await createResponse.Content.ReadAsStringAsync();

            using var createDoc = JsonDocument.Parse(createJson);

            int clientId = createDoc.RootElement.GetProperty("id").GetInt32();

            var readResponse = await _client.GetAsync($"api/Clients/{clientId}");

            Assert.That(readResponse.StatusCode, Is.EqualTo(HttpStatusCode.OK));

            var readJson = await readResponse.Content.ReadAsStringAsync();

            Assert.That(readJson, Does.Contain("Integration Test Client"));
            Assert.That(readJson, Does.Contain("Western Cape"));
        }

        [Test]
        public async Task CreateClient_ThenCreateContract_ThenReadContract_ShouldVerifyDataIntegrity()
        {
            var newClient = new
            {
                name = "Contract Test Client",
                email = $"contract{Guid.NewGuid()}@client.co.za",
                phoneNumber = "0215558888",
                region = "Gauteng"
            };

            var clientResponse = await _client.PostAsJsonAsync("api/Clients", newClient);

            Assert.That(clientResponse.StatusCode, Is.EqualTo(HttpStatusCode.Created));

            var clientJson = await clientResponse.Content.ReadAsStringAsync();

            using var clientDoc = JsonDocument.Parse(clientJson);

            int clientId = clientDoc.RootElement.GetProperty("id").GetInt32();

            var newContract = new
            {
                clientId = clientId,
                startDate = DateTime.Now,
                endDate = DateTime.Now.AddMonths(6),
                status = "Active",
                serviceLevel = "Premium",
                filePath = "/uploads/contracts/integration-test.pdf"
            };

            var contractResponse = await _client.PostAsJsonAsync("api/Contracts", newContract);

            Assert.That(contractResponse.StatusCode, Is.EqualTo(HttpStatusCode.Created));

            var contractJson = await contractResponse.Content.ReadAsStringAsync();

            using var contractDoc = JsonDocument.Parse(contractJson);

            int contractId = contractDoc.RootElement.GetProperty("id").GetInt32();

            var readContractResponse = await _client.GetAsync($"api/Contracts/{contractId}");

            Assert.That(readContractResponse.StatusCode, Is.EqualTo(HttpStatusCode.OK));

            var readContractJson = await readContractResponse.Content.ReadAsStringAsync();

            Assert.That(readContractJson, Does.Contain("Premium"));
            Assert.That(readContractJson, Does.Contain("Active"));
        }

        [Test]
        public async Task PatchContractStatus_ShouldUpdateStatusToApproved()
        {
            var newClient = new
            {
                name = "Patch Test Client",
                email = $"patch{Guid.NewGuid()}@client.co.za",
                phoneNumber = "0215557777",
                region = "KwaZulu-Natal"
            };

            var clientResponse = await _client.PostAsJsonAsync("api/Clients", newClient);

            Assert.That(clientResponse.StatusCode, Is.EqualTo(HttpStatusCode.Created));

            var clientJson = await clientResponse.Content.ReadAsStringAsync();

            using var clientDoc = JsonDocument.Parse(clientJson);

            int clientId = clientDoc.RootElement.GetProperty("id").GetInt32();

            var newContract = new
            {
                clientId = clientId,
                startDate = DateTime.Now,
                endDate = DateTime.Now.AddMonths(6),
                status = "Active",
                serviceLevel = "Standard",
                filePath = "/uploads/contracts/patch-test.pdf"
            };

            var contractResponse = await _client.PostAsJsonAsync("api/Contracts", newContract);

            Assert.That(contractResponse.StatusCode, Is.EqualTo(HttpStatusCode.Created));

            var contractJson = await contractResponse.Content.ReadAsStringAsync();

            using var contractDoc = JsonDocument.Parse(contractJson);

            int contractId = contractDoc.RootElement.GetProperty("id").GetInt32();

            var patchResponse = await _client.PatchAsJsonAsync(
                $"api/Contracts/{contractId}/status",
                "Approved"
            );

            Assert.That(patchResponse.StatusCode, Is.EqualTo(HttpStatusCode.OK));

            var updatedResponse = await _client.GetAsync($"api/Contracts/{contractId}");

            Assert.That(updatedResponse.StatusCode, Is.EqualTo(HttpStatusCode.OK));

            var updatedJson = await updatedResponse.Content.ReadAsStringAsync();

            Assert.That(updatedJson, Does.Contain("Approved"));
        }

        [Test]
        public async Task GetNonExistingContract_ShouldReturnNotFound()
        {
            var response = await _client.GetAsync("api/Contracts/999999");

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
        }
    }
}
/////////////////////////////////////////// ----- End Of File ----- ///////////////////////////////////////////