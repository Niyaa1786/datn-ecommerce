using ECommerce.Application.Common.DTOs;
using ECommerce.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Application.Features.Categories.GetCategories
{
    public class GetCategoriesUseCase : IUseCase<GetCategoriesRequest, GetCategoriesResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetCategoriesUseCase(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<GetCategoriesResponse> ExecuteAsync(GetCategoriesRequest request, CancellationToken ct = default)
        {
            var categories = await _unitOfWork.Categories.GetAllWithFiltersAsync(
                request.Keyword,
                request.IncludeDeleted,
                request.Page,
                request.PageSize,
                ct);

            var totalCount = await _unitOfWork.Categories.CountAsync(request.IncludeDeleted, ct);
            var items = categories.Select(c => new CategoryDto
            {
                Id = c.Id,
                Name = c.Name,
                Description = c.Description,
                CreatedAt = c.CreatedAt,
                UpdatedAt = c.UpdatedAt,
            }).ToList();


            return new GetCategoriesResponse
            {
                Items = items,
                Page = request.Page,
                PageSize = request.PageSize,
                TotalCount = totalCount
            };
        }
    }
}
