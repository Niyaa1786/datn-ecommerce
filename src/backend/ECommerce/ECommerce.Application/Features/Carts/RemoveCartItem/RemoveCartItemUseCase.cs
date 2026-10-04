using FluentValidation;
using Microsoft.VisualBasic;
using ECommerce.Application.Common.Exceptions;
using ECommerce.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Application.Features.Carts.RemoveCartItem
{
    public class RemoveCartItemUseCase : IUseCase<RemoveCartItemRequest, RemoveCartItemResponse>
    {
        private readonly IUnitOfWork _unitOfWork;

        public RemoveCartItemUseCase(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<RemoveCartItemResponse> ExecuteAsync(RemoveCartItemRequest request, CancellationToken ct = default)
        {
            var cart = await _unitOfWork.Carts.GetByUserIdWithDetailsAsync(request.UserId, ct);
            if (cart == null)
                throw new NotFoundException("Cart not found.");

            cart.RemoveItem(request.CartItemId);

            await _unitOfWork.SaveChangesAsync(ct);

            return new RemoveCartItemResponse();
        }
    }
}
