using System.Text.Json.Serialization;

public class ProductModel
{
    [JsonPropertyName("id")]
    public string Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("price")]
    public string Price { get; set; }

    [JsonPropertyName("img")]
    public string Img { get; set; }

    [JsonPropertyName("description")]
    public string Description { get; set; }

    [JsonPropertyName("type")]
    public string Type { get; set; }

    [JsonPropertyName("deleted")]
    public bool Deleted { get; set; }
}