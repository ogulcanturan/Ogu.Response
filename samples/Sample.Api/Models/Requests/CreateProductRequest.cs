namespace Sample.Api.Models.Requests;

public class CreateProductRequest
{
    public string Name { get; set; }

    public string Price { get; set; }

    public string Category { get; set; }

    public string Sku { get; set; }
}