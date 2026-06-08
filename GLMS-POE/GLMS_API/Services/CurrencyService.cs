using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace GLMS_API.Services
{
    public class CurrencyService
    {
        // SINGLETON INSTANCE
        private static CurrencyService _instance;
        private static readonly object _lock = new object();

        private readonly HttpClient _httpClient;

        // PRIVATE constructor
        private CurrencyService()
        {
            _httpClient = new HttpClient();
        }

        // ACCESS POINT
        public static CurrencyService Instance
        {
            get
            {
                lock (_lock)
                {
                    if (_instance == null)
                        _instance = new CurrencyService();

                    return _instance;
                }
            }
        }

        // API CALL: Convert to ZAR
        public async Task<decimal> ConvertToZAR(string currencyCode, decimal amount)
        {
            if (currencyCode == "ZAR")
                return amount;

            string url = $"https://open.er-api.com/v6/latest/{currencyCode}";

            var response = await _httpClient.GetStringAsync(url);

            using var doc = JsonDocument.Parse(response);

            // 🔥 Check if API response is OK
            if (!doc.RootElement.TryGetProperty("result", out JsonElement result)
                || result.GetString() != "success")
            {
                return amount; // fallback
            }

            // 🔥 Get rates
            if (!doc.RootElement.TryGetProperty("rates", out JsonElement rates))
            {
                return amount;
            }

            if (!rates.TryGetProperty("ZAR", out JsonElement zarRate))
            {
                return amount;
            }

            decimal rate = zarRate.GetDecimal();

            return amount * rate;
        }
    }
}
/////////////////////////////////////////// ----- End Of File ----- ///////////////////////////////////////////