using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Application.Features.Carts.AddToCart
{
    public class AddToCartValidator : AbstractValidator<AddToCartRequest>
    {
        public AddToCartValidator()
        {
            RuleFor(x => x.ProductVariantId)
                .NotEmpty().WithMessage("Product variant ID is required.");

            RuleFor(x => x.Quantity)
                .GreaterThan(0).WithMessage("Quantity must be greater than 0.");
        }
    }
}
