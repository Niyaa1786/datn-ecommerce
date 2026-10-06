using ECommerce.Application.Common.Exceptions;
using ECommerce.Application.Common.Interfaces;
using ECommerce.Domain.Entities;

namespace ECommerce.Application.Features.Coupons.GetCoupon
{
    public class GetCouponUseCase(IUnitOfWork unitOfWork) : IUseCase<GetCouponRequest, GetCouponResponse>
    {
        public async Task<GetCouponResponse> ExecuteAsync(GetCouponRequest request, CancellationToken ct = default)
        {
            var coupon = await unitOfWork.Coupons.GetByIdAsync(request.Id, ct);
            if (coupon == null)
                throw new NotFoundException(nameof(Coupon), request.Id);

            return new GetCouponResponse
            {
                Id = coupon.Id,
                Code = coupon.Code,
                Description = coupon.Description,
                DiscountType = coupon.DiscountType.ToString(),
                DiscountValue = coupon.DiscountValue,
                MinOrderAmount = coupon.MinOrderAmount,
                MaxDiscountAmount = coupon.MaxDiscountAmount,
                Quantity = coupon.Quantity,
                UsedCount = coupon.UsedCount,
                StartDate = coupon.StartDate,
                EndDate = coupon.EndDate,
                IsActive = coupon.IsActive,
                CreatedAt = coupon.CreatedAt
            };
        }
    }
}
