using System.Text.Json.Serialization;

namespace ECommerce.Application.Features.Reviews.CreateReview
{
    public class CreateReviewRequest
    {
        [JsonIgnore]
        public Guid UserId { get; set; }
        public int OrderItemId { get; set; }
        public int Rating { get; set; }
        public string? Comment { get; set; }
    }
}
