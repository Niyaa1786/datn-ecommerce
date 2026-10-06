using System.Text.Json.Serialization;

namespace ECommerce.Application.Features.Coupons.ValidateCoupon
{
    public class ValidateCouponRequest
    {
        [JsonIgnore]
        public Guid UserId { get; set; }
        public string Code { get; set; } = string.Empty;
    }
}
