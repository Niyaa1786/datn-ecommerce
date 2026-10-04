using ECommerce.Application.Common.DTOs;
using ECommerce.Application.Common.Exceptions;
using ECommerce.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Application.Features.Products.GetProduct
{
    public class GetProductUseCase : IUseCase<GetProductRequest, GetProductResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetProductUseCase(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<GetProductResponse> ExecuteAsync(GetProductRequest request, CancellationToken ct = default)
        {
            var product = await _unitOfWork.Products.GetByIdWithDetailsAsync(request.Id, ct);
            if (product == null || product.IsDeleted)
                throw new NotFoundException(nameof(Products), request.Id);

            return new GetProductResponse
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Status = product.Status.ToString(),
                CategoryId = product.CategoryId,
                CategoryName = product.Category?.Name,
                CreatedAt = product.CreatedAt,
                UpdatedAt = product.UpdatedAt,
                Variants = product.Variants.Select(v => new ProductVariantDto
                {
                    Id = v.Id,
                    SKU = v.SKU,
                    Price = v.Price,
                    Stock = v.Stock,
                    Size = v.Size,
                    Color = v.Color,
                    CreatedAt = v.CreatedAt,
                    UpdatedAt = v.UpdatedAt
                }).ToList(),
                Images = product.Images.Select(i => new ProductImageDto
                {
                    Id = i.Id,
                    ImageUrl = i.ImageUrl,
                    IsThumbnail = i.IsThumbnail,
                    PublicId = i.PublicId
                }).ToList()
            };
        }
    }
}
