using FluentValidation;
using ECommerce.Application.Common.Exceptions;
using ECommerce.Application.Common.Interfaces;
using ECommerce.Domain.Entities;

namespace ECommerce.Application.Features.Coupons.ValidateCoupon
{
    public class ValidateCouponUseCase(IUnitOfWork unitOfWork, IValidator<ValidateCouponRequest> validator) : IUseCase<ValidateCouponRequest, ValidateCouponResponse>
    {
        public async Task<ValidateCouponResponse> ExecuteAsync(ValidateCouponRequest request, CancellationToken ct = default)
        {
            validator.ValidateAndThrow(request);

            var cart = await unitOfWork.Carts.GetByUserIdWithDetailsAsync(request.UserId, ct);
            if (cart == null || cart.Items.Count == 0)
                throw new AppValidationException(nameof(Cart), "Cart is empty.");

            var coupon = await unitOfWork.Coupons.GetByCodeAsync(request.Code, ct);
            if (coupon == null)
                throw new NotFoundException("Coupon code is invalid.");

            var orderAmount = cart.TotalPrice;
            var discountAmount = coupon.CalculateDiscount(orderAmount);

            return new ValidateCouponResponse
            {
                Code = coupon.Code,
                DiscountType = coupon.DiscountType.ToString(),
                DiscountValue = coupon.DiscountValue,
                OrderAmount = orderAmount,
                DiscountAmount = discountAmount,
                FinalAmount = orderAmount - discountAmount
            };
        }
    }
}
