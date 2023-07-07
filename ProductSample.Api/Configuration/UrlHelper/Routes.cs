namespace ProductSample.Api.Configuration.UrlHelper;

public static class Routes
{
    public const string BaseUrl = "api/v{version:apiversion}/";

    public static class Product
    {
        public const string Insert = BaseUrl + "product/insert";
        public const string Upload = BaseUrl + "product/upload";
        public const string Update = BaseUrl + "product/update";
        public const string DisCount = BaseUrl + "product/discount";
        public const string GetAll = BaseUrl + "product/get-all";
    }
}
