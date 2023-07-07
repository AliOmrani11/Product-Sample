using Product.Application.Dto.Response.Accessory;

namespace Product.Application.Dto.Response.Product;

public class ProductGroupResponse
{
    public string Name { get; set; }
    public string PhotoUrl { get; set; }
    public decimal DiscountPrice { get; set; }
    public List<ProductResponse> Products { get; set; }
    public List<AccessoryResponse> Accessories { get; set; }
}
