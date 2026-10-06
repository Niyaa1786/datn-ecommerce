using System.Text.Json.Serialization;

namespace ECommerce.Application.Features.Coupons.DeleteCoupon
{
    public class DeleteCouponRequest
    {
        [JsonIgnore]
        public int Id { get; set; }
    }
}
