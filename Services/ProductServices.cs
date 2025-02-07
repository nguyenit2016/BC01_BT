public class ProductServices {
    public List<ProductVM> products{get;set;} = new List<ProductVM>();
    public ProductVM productDetail{get;set;} = new ProductVM();
    HttpClient _httpClient;
    public ProductServices(HttpClient httpClient){
        _httpClient = httpClient;
    }

    public async Task GetProducts(){
        var url = "https://apistore.cybersoft.edu.vn/api/Product";
        var request = new HttpRequestMessage(HttpMethod.Get, url);
        var response = await _httpClient.SendAsync(request);
        response.EnsureSuccessStatusCode();
        var res = response.Content.ReadFromJsonAsync<HttpResponseBase<List<ProductVM>>>().Result;
        products = res.content;
        NotifyStateChange();
    }
    public async void GetProductById(string id){
        var url = $"https://apistore.cybersoft.edu.vn/api/Product/getid?id={id}";
        var request = new HttpRequestMessage(HttpMethod.Get, url);
        var response = await _httpClient.SendAsync(request);
        response.EnsureSuccessStatusCode();
        var res = response.Content.ReadFromJsonAsync<HttpResponseBase<ProductVM>>().Result;
        productDetail = res.content;
        NotifyStateChange();
    }
    
    public async Task<string> UpdateProduct(){
        var url = "https://apistore.cybersoft.edu.vn/api/Product/updateProduct";
        var request = await _httpClient.PutAsJsonAsync(url, productDetail);
        var response = await request.Content.ReadFromJsonAsync<HttpResponseBase<string>>();
        NotifyStateChange();
        return response.content;
    }
    public async Task<string> DeleteProduct(double id){
        var url = $"https://apistore.cybersoft.edu.vn/api/Product/{id}";
        var request = new HttpRequestMessage(HttpMethod.Delete, url);
        var response = await _httpClient.SendAsync(request);
        response.EnsureSuccessStatusCode();
        var res = response.Content.ReadFromJsonAsync<HttpResponseBase<string>>().Result;
        NotifyStateChange();
        GetProducts();
        return res.content;
    }
    public event Action OnChange;
    public void NotifyStateChange() => OnChange?.Invoke();
}