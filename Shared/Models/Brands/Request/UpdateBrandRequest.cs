namespace Shared.Models.Brands.Request
{
    public class UpdateBrandRequest
    {
        public Guid BrandId { get; set; }
        public string Name { get; set; } = string.Empty;

    }
}
