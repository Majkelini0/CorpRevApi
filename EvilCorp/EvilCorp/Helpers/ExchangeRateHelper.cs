using Newtonsoft.Json.Linq;

namespace EvilCorp.Helpers;

public class ExchangeRateHelper
{
    private async Task<decimal> GetExchangeRate(string currencyCode)
    {
        HttpClient client = new HttpClient();
        currencyCode = currencyCode.ToUpper();
        string key = "3335b740bf7fa2f0194b29a9";
        string url = $"https://v6.exchangerate-api.com/v6/{key}/latest/PLN";
        
        HttpResponseMessage response = await client.GetAsync(url);
        response.EnsureSuccessStatusCode();

        string responsee = await response.Content.ReadAsStringAsync();
        JObject json = JObject.Parse(responsee);
        
        if (json["conversion_rates"][currencyCode] == null)
        {
            throw new Exception($"Currency code: {currencyCode} does not exist");
        }

        decimal rate = Convert.ToDecimal(json["conversion_rates"][currencyCode]);
        
        return rate;
    }

    public async Task<decimal> GetExchangedIncome(decimal income, string countryCode)
    {
        if (countryCode != "PLN")
        {
            decimal rate;
            try
            {
                rate = await GetExchangeRate(countryCode);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return Math.Round((income * rate), 2);
        }

        return Math.Round(income, 2);
    }
}