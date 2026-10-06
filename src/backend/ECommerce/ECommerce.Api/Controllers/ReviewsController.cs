using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ECommerce.Api.Responses;
using ECommerce.Application.Features.Reviews.CreateReview;
using ECommerce.Application.Features.Reviews.GetReviewsByProduct;
using System.Security.Claims;

namespace ECommerce.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ReviewsController : ControllerBase
    {
        #region
        private readonly CreateReviewUseCase _createReviewUseCase;
        private readonly GetReviewsByProductUseCase _getReviewsByProductUseCase;

        public ReviewsController(
            CreateReviewUseCase createReviewUseCase,
            GetReviewsByProductUseCase getReviewsByProductUseCase)
        {
            _createReviewUseCase = createReviewUseCase;
            _getReviewsByProductUseCase = getReviewsByProductUseCase;
        }
        #endregion

        [HttpPost]
        public async Task<IActionResult> CreateReview(CreateReviewRequest request, CancellationToken ct)
        {
            request.UserId = GetUserId();
            var result = await _createReviewUseCase.ExecuteAsync(request, ct);

            return Ok(ApiResponse<CreateReviewResponse>.Success(result, result.Message));
        }

        [HttpGet("product/{productId:guid}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetReviewsByProduct(Guid productId, [FromQuery] GetReviewsByProductRequest request, CancellationToken ct)
        {
            request.ProductId = productId;
            var result = await _getReviewsByProductUseCase.ExecuteAsync(request, ct);

            return Ok(ApiResponse<GetReviewsByProductResponse>.Success(result));
        }

        private Guid GetUserId()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
                throw new UnauthorizedAccessException("User ID claim not found.");
            return Guid.Parse(userId);
        }
    }
}
