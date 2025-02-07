
using Microsoft.AspNetCore.WebUtilities;

public class CryptoServices
{
    HttpClient _httpClient;
    public CryptoServices(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public List<Coin> lstCoin = new List<Coin>();
    public List<Coin> lstCoinFavorite = new List<Coin>();

    public async void GetCoinAsync()
    {
        // url api
        var url = "https://api.coingecko.com/api/v3/coins/markets";
        var parameters = new Dictionary<string, string> {
            {"vs_currency", "usd"},
            {"order", "market_cap_desc"},
            {"per_page", "10"},
            {"page", "1"},
            {"sparkline", "false"},
        };
        var uri = QueryHelpers.AddQueryString(url, parameters);
        var request = new HttpRequestMessage(HttpMethod.Get, uri);
        request.Headers.Add("User-Agent", "YourAppName/1.0");
        var response = await _httpClient.SendAsync(request);
        response.EnsureSuccessStatusCode();
        lstCoin = response.Content.ReadFromJsonAsync<List<Coin>>().Result;
        NotifyStateChange();
    }
    public void AddFavorite(string id)
    {
        var coin = lstCoinFavorite.Find(x => x.id == id);
        if (coin == null)
        {
            var item = lstCoin.Find(x => x.id == id);
            if (item != null)
            {
                lstCoinFavorite.Add(item);
            }
        }
        NotifyStateChange();
    }
    public void RemoveFavorite(string id)
    {
        var coin = lstCoinFavorite.Find(x => x.id == id);
        if (coin != null)
        {
            lstCoinFavorite.Remove(coin);
        }
        NotifyStateChange();
    }
    public event Action OnChange;
    public void NotifyStateChange() => OnChange?.Invoke();
}