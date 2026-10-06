using System.Text.Json.Serialization;

namespace ECommerce.Application.Features.Reviews.GetReviewsByProduct
{
    public class GetReviewsByProductRequest
    {
        [JsonIgnore]
        public Guid ProductId { get; set; }
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
