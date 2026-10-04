using FluentValidation;

namespace ECommerce.Application.Features.Products.UpdateProductImages
{
    public class UpdateProductImagesValidator : AbstractValidator<UpdateProductImagesRequest>
    {
        public UpdateProductImagesValidator()
        {
            RuleFor(x => x.ProductId).NotEmpty().WithMessage("Product Id is required.");
        }
    }
}
