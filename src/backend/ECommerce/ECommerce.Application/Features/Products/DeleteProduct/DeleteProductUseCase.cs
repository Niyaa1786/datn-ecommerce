using ECommerce.Application.Common.Exceptions;
using ECommerce.Application.Common.Interfaces;
using ECommerce.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Application.Features.Products.DeleteProduct
{
    public class DeleteProductUseCase : IUseCase<DeleteProductRequest, DeleteProductResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        public DeleteProductUseCase(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<DeleteProductResponse> ExecuteAsync(DeleteProductRequest request, CancellationToken ct = default)
        {
            var product = await _unitOfWork.Products.GetByIdAsync(request.Id, ct);
            if (product == null || product.IsDeleted)
                throw new NotFoundException(nameof(Product), request.Id);

            product.SoftDelete();

            await _unitOfWork.SaveChangesAsync(ct);

            return new DeleteProductResponse();
        }
    }
}
