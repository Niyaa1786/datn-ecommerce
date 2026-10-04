using Microsoft.AspNetCore.Http;
using System.Text.Json.Serialization;

namespace ECommerce.Application.Features.Products.UpdateProductImages
{
    public class UpdateProductImagesRequest
    {
        [JsonIgnore]
        public Guid ProductId { get; set; }
        public List<IFormFile> Images { get; set; } = new();
    }
}
