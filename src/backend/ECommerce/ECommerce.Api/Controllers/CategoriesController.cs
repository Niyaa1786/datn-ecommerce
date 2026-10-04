using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ECommerce.Api.Responses;
using ECommerce.Application.Features.Categories.CreateCategory;
using ECommerce.Application.Features.Categories.DeleteCategory;
using ECommerce.Application.Features.Categories.GetCategories;
using ECommerce.Application.Features.Categories.GetCategory;
using ECommerce.Application.Features.Categories.UpdateCategory;
using ECommerce.Domain.Enums;

namespace ECommerce.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = nameof(UserRole.Admin))]
    public class CategoriesController : ControllerBase
    {
        #region
        private readonly CreateCategoryUseCase _createUseCase;
        private readonly UpdateCategoryUseCase _updateUseCase;
        private readonly DeleteCategoryUseCase _deleteUseCase;
        private readonly GetCategoryUseCase _getCategoryUseCase;
        private readonly GetCategoriesUseCase _getCategoriesUseCase;

        public CategoriesController(
            CreateCategoryUseCase createUseCase,
            UpdateCategoryUseCase updateUseCase,
            DeleteCategoryUseCase deleteUseCase,
            GetCategoryUseCase getCategoryUseCase,
            GetCategoriesUseCase getCategoriesUseCase)
        {
            _createUseCase = createUseCase;
            _updateUseCase = updateUseCase;
            _deleteUseCase = deleteUseCase;
            _getCategoryUseCase = getCategoryUseCase;
            _getCategoriesUseCase = getCategoriesUseCase;
        }

        #endregion

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetCategories([FromQuery] GetCategoriesRequest request, CancellationToken ct)
        {
            var result = await _getCategoriesUseCase.ExecuteAsync(request, ct);

            return Ok(ApiResponse<GetCategoriesResponse>.Success(result));
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetCategory(int id, CancellationToken ct)
        {
            var request = new GetCategoryRequest { Id = id };
            var result = await _getCategoryUseCase.ExecuteAsync(request, ct);

            return Ok(ApiResponse<GetCategoryResponse>.Success(result));
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory(CreateCategoryRequest request, CancellationToken ct)
        {
            var result = await _createUseCase.ExecuteAsync(request, ct);

            return Ok(ApiResponse<CreateCategoryResponse>.Success(result, "Category created."));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(int id, UpdateCategoryRequest request, CancellationToken ct)
        {
            request.Id = id;
            var result = await _updateUseCase.ExecuteAsync(request, ct);

            return Ok(ApiResponse<UpdateCategoryResponse>.Success(result, "Category updated."));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id, CancellationToken ct)
        {
            var request = new DeleteCategoryRequest { Id = id };
            var result = await _deleteUseCase.ExecuteAsync(request, ct);

            return Ok(ApiResponse<DeleteCategoryResponse>.Success(result, result.Message));
        }
    }
}
