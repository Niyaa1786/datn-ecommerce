using FluentValidation;
using ECommerce.Application.Common.Exceptions;
using ECommerce.Application.Common.Interfaces;
using ECommerce.Domain.Entities;

namespace ECommerce.Application.Features.Reviews.GetReviewsByProduct
{
    public class GetReviewsByProductUseCase(IUnitOfWork unitOfWork, IValidator<GetReviewsByProductRequest> validator) : IUseCase<GetReviewsByProductRequest, GetReviewsByProductResponse>
    {
        public async Task<GetReviewsByProductResponse> ExecuteAsync(GetReviewsByProductRequest request, CancellationToken ct = default)
        {
            validator.ValidateAndThrow(request);

            var product = await unitOfWork.Products.GetByIdAsync(request.ProductId, ct);
            if (product == null)
                throw new NotFoundException(nameof(Product), request.ProductId);

            var reviews = await unitOfWork.Reviews.GetByProductIdAsync(request.ProductId, request.Page, request.PageSize, ct);
            var totalCount = await unitOfWork.Reviews.CountByProductIdAsync(request.ProductId, ct);
            var averageRating = await unitOfWork.Reviews.GetAverageRatingByProductIdAsync(request.ProductId, ct);

            return new GetReviewsByProductResponse
            {
                AverageRating = Math.Round(averageRating, 1),
                Items = reviews.Select(r => new ReviewDto
                {
                    Id = r.Id,
                    UserFullName = r.User.FullName,
                    Rating = r.Rating,
                    Comment = r.Comment,
                    SKU = r.OrderItem.SKU,
                    CreatedAt = r.CreatedAt
                }).ToList(),
                Page = request.Page,
                PageSize = request.PageSize,
                TotalCount = totalCount
            };
        }
    }
}
