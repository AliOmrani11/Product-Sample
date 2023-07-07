namespace Product.Application.Dto.Response.Product;

public class ProductResponse
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal? Weight { get; set; }
    public int? Inventory { get; set; }
    public decimal Amount { get; set; }
    public List<ProductPropertyResponse> Properties { get; set; }
}
