using ECommerce.Application.Common.DTOs;
using ECommerce.Application.Common.Interfaces;
using ECommerce.Application.Features.Products.GetProduct;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Application.Features.Products.GetProducts
{
    public class GetProductsUseCase : IUseCase<GetProductsRequest, GetProductsResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetProductsUseCase(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<GetProductsResponse> ExecuteAsync(GetProductsRequest request, CancellationToken ct = default)
        {
            var product = await _unitOfWork.Products.GetAllByFiltersAsync(
                request.CategoryId,
                request.Keyword,
                request.Status,
                request.IncludeDeleted,
                request.Page,
                request.PageSize,
                ct);

            var totalCount = await _unitOfWork.Products.CountProductsAsync(
                request.CategoryId,
                request.Keyword,
                request.Status,
                request.IncludeDeleted,
                ct);

            var items = product.Select(p => new ProductListDto
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Status = p.Status,
                CategoryName = p.Category?.Name,
                MinPrice = p.GetCheapestPrice(),
                ThumbnailUrl = p.Images.FirstOrDefault(i => i.IsThumbnail)?.ImageUrl ?? p.Images.FirstOrDefault()?.ImageUrl,
                CreatedAt = p.CreatedAt
            }).ToList();

            return new GetProductsResponse
            {
                Items = items,
                TotalCount = totalCount,
                Page = request.Page,
                PageSize = request.PageSize
            };
        }
    }
}
