using System.Text.Json.Serialization;

namespace ECommerce.Application.Features.Coupons.GetCoupon
{
    public class GetCouponRequest
    {
        [JsonIgnore]
        public int Id { get; set; }
    }
}
